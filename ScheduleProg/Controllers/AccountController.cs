using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleProg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScheduleProg.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ScheduleProg.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context=context;
        }

        public IActionResult Index()
        {
           
            ViewData["Subgroup_Id"] = new SelectList(_context.Subgroups, "Id", "Subgr_Name");
            return View();
        }

        [Authorize(Roles = "Адміністратор")]

        [HttpGet]
        public IActionResult CreateStudent()
        {
            ViewData["Subgroup_Id"] = new SelectList(_context.Subgroups, "Id", "Subgr_Name");
            return View();
        }

        [Authorize(Roles = "Адміністратор")]
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudent model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName= model.Email, First_Name=model.First_Name, Last_Name=model.Last_Name, Email= model.Email };
                // добавляем пользователя
                
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Студент");
                    Student student = new Student { First_Name = model.First_Name, Last_Name = model.Last_Name, Subgroup_Id = model.Subgroup_Id,User_Id= user.Id };
                    _context.Add(student);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        /// <summary>

        // GET: PairTimes/Delete/5

        [Authorize(Roles = "Адміністратор")]
        public IActionResult DeleteUser()
        {
            
            ViewData["User_id"] = new SelectList(_context.Users,"Id","Email");


            return View();
        }

        // POST: PairTimes/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string User_id)
        {
            var user = await _userManager.FindByIdAsync(User_id);
            var Student = await _context.Students.FirstOrDefaultAsync(p=>p.User_Id==User_id);

            var Teacher= await _context.Teachers.FirstOrDefaultAsync(p => p.User_Id == User_id);
            if (Student != null)
            {
                _context.Students.Remove(Student);
                await _userManager.DeleteAsync(user);
                await _context.SaveChangesAsync();
            }
            if (Teacher != null)
            {
                _context.Teachers.Remove(Teacher);
                await _userManager.DeleteAsync(user);
                await _context.SaveChangesAsync();
            }
            else {
                await _userManager.DeleteAsync(user);
                await _context.SaveChangesAsync();
            }
            return View("Home/AdminPage");
        }


        
        [Authorize(Roles = "Адміністратор")]
        [HttpGet]
        public IActionResult CreateTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacher model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Email, First_Name = model.First_Name, Last_Name = model.Last_Name, Email = model.Email };
                // добавляем пользователя

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Вчитель");
                    Teacher teacher= new Teacher{ First_Name = model.First_Name, Last_Name = model.Last_Name, User_Id = user.Id };
                    _context.Add(teacher);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Адміністратор")]
        public IActionResult CreateAdministrator()
        {
            return View();
        }

        [Authorize(Roles = "Адміністратор")]
        [HttpPost]
        public async Task<IActionResult> CreateAdministrator(CreateAdministrator model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Email, First_Name = model.First_Name, Last_Name = model.Last_Name, Email = model.Email };
                // добавляем пользователя

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Адміністратор");
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }




    }
}
