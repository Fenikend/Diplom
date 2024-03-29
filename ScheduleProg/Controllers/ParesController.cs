﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleProg.Data;
using ScheduleProg.Models;

namespace ScheduleProg.Controllers
{
    [Authorize]
    public class ParesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pares
        [Authorize(Roles = "Адміністратор")]

        public async Task<IActionResult> AdminView()
        {
            ViewBag.admin_item = _context.Schedules
                               .Include(p => p.PairTime)
                               .Include(p => p.Semester)
                               .Include(p => p.Subject)
                               .Include(p => p.Teacher);
            return View(ViewBag.admin_item);
        }
        /*public async Task<IActionResult> AdminView()
        {
            return _context.Schedules != null ?
                        View(await _context.Schedules.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Teachers'  is null.");
        }*/
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Адміністратор"))
            {
                return RedirectToAction(nameof(AdminView));
            }
            else
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //var StudId = await _context.Students.FindAsync(userId);

                var TeacherId = _context.Teachers.Where(p => p.User_Id == userId).Select(p => p.Id).FirstOrDefault();
                var StudentSubgrId = _context.Students.Where(p => p.User_Id == userId).Select(p => p.Subgroup_Id).FirstOrDefault();
                var CurPareIDs = _context.PareSubgroups.Where(p => p.Subgroup_Id == StudentSubgrId).Select(p => p.Pare_Id).ToList();
                if (TeacherId != 0)
                {

                    ViewBag.monday_item = _context.Schedules
                               .Include(p => p.PairTime)
                               .Include(p => p.Semester)
                               .Include(p => p.Subject)
                               .Include(p => p.Teacher)
                               .Where(p => p.Week_Day == "Понеділок")
                    .Where(p => p.Teacher_Id == TeacherId)
                    .OrderBy(p => p.PairTime.Id);

                    ViewBag.tuesday_item = _context.Schedules
                               .Include(p => p.PairTime)
                               .Include(p => p.Semester)
                               .Include(p => p.Subject)
                               .Include(p => p.Teacher)
                    .Where(p => p.Week_Day == "Вівторок")
                    .Where(p => p.Teacher_Id == TeacherId)
                    .OrderBy(p => p.PairTime.Id);

                    ViewBag.wednesday_item = _context.Schedules
                               .Include(p => p.PairTime)
                               .Include(p => p.Semester)
                               .Include(p => p.Subject)
                               .Include(p => p.Teacher)
                    .Where(p => p.Week_Day == "Середа")
                    .Where(p => p.Teacher_Id == TeacherId)
                    .OrderBy(p => p.PairTime.Id);

                    ViewBag.thursday_item = _context.Schedules
                                   .Include(p => p.PairTime)
                                   .Include(p => p.Semester)
                                   .Include(p => p.Subject)
                                   .Include(p => p.Teacher)
                        .Where(p => p.Week_Day == "Четвер")
                        .Where(p => p.Teacher_Id == TeacherId)
                        .OrderBy(p => p.PairTime.Id);

                    ViewBag.friday_item = _context.Schedules
                                   .Include(p => p.PairTime)
                                   .Include(p => p.Semester)
                                   .Include(p => p.Subject)
                                   .Include(p => p.Teacher)
                        .Where(p => p.Week_Day == "П'ятниця")
                        .Where(p => p.Teacher_Id == TeacherId)
                        .OrderBy(p => p.PairTime.Id);
                }
                if (StudentSubgrId != 0)
                {
                    //var Cont = _context.Pares.FromSqlInterpolated($"Select * from Pare Where Week_Day=N'Понеділок' and Id in{CurPareID}");


                    ViewBag.monday_item = _context.Schedules
                                   .Include(p => p.PairTime)
                                   .Include(p => p.Semester)
                                   .Include(p => p.Subject)
                                   .Include(p => p.Teacher)
                        .Where(p => p.Week_Day == "Понеділок")
                        .Where(p => CurPareIDs.Contains(p.Id))
                        .OrderBy(p => p.PairTime.Id)
                        ;

                    ViewBag.tuesday_item = _context.Schedules
                                   .Include(p => p.PairTime)
                                   .Include(p => p.Semester)
                                   .Include(p => p.Subject)
                                   .Include(p => p.Teacher)
                        .Where(p => p.Week_Day == "Вівторок")
                        .Where(p => CurPareIDs.Contains(p.Id))
                        .OrderBy(p => p.PairTime.Id);

                    ViewBag.wednesday_item = _context.Schedules
                                   .Include(p => p.PairTime)
                                   .Include(p => p.Semester)
                                   .Include(p => p.Subject)
                                   .Include(p => p.Teacher)
                        .Where(p => p.Week_Day == "Середа")
                        .Where(p => CurPareIDs.Contains(p.Id))
                        .OrderBy(p => p.PairTime.Id);

                    ViewBag.thursday_item = _context.Schedules
                                   .Include(p => p.PairTime)
                                   .Include(p => p.Semester)
                                   .Include(p => p.Subject)
                                   .Include(p => p.Teacher)
                        .Where(p => p.Week_Day == "Четвер")
                        .Where(p => CurPareIDs.Contains(p.Id))
                        .OrderBy(p => p.PairTime.Id);

                    ViewBag.friday_item = _context.Schedules
                                   .Include(p => p.PairTime)
                                   .Include(p => p.Semester)
                                   .Include(p => p.Subject)
                                   .Include(p => p.Teacher)
                        .Where(p => p.Week_Day == "П'ятниця")
                        .Where(p => CurPareIDs.Contains(p.Id))
                        .OrderBy(p => p.PairTime.Id);
                }
                var applicationDbContext = _context.Schedules
                               .Include(p => p.PairTime)
                               .Include(p => p.Semester)
                               .Include(p => p.Subject)
                               .Include(p => p.Teacher);
                //.Where(p=>p.Week_Day=="tuesday");
                var p = applicationDbContext.ToList();
                return View();
            }
        }


        // GET: Pares/Details/5

        // GET: Pares/Create
        [Authorize(Roles = "Адміністратор")]
        public IActionResult Create()
        {
               
            ViewData["Pair_Time_Id"] = new SelectList(_context.PairTimes, "Id", "Id");
            var view1 = new SelectList(_context.Subject, "Id", "Discipline_Name");
            var view2 = new SelectList(_context.Semesters, "Id", "Id");
            var view3 = new SelectList(_context.Teachers, "Id", "Full_name");
           
            ViewData["Subject_Id"]= view1;
            ViewData["Semester_Id"] = view2;
            ViewData["Teacher_Id"] = view3;
            return View();
        }

        // POST: Pares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Адміністратор")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject_Id,Semester_Id,Teacher_Id,Pair_Time_Id,Week_Day")] Pare pare)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pair_Time_Id"] = new SelectList(_context.PairTimes, "Id", "Id", pare.Pair_Time_Id);
            ViewData["Semester_Id"] = new SelectList(_context.Semesters, "Id", "Id", pare.Semester_Id);
            ViewData["Subject_Id"] = new SelectList(_context.Subject, "Id", "Id", pare.Subject_Id);
            ViewData["Teacher_Id"] = new SelectList(_context.Teachers, "Id", "Id", pare.Teacher_Id);
            return View(pare);
        }

        // GET: Pares/Edit/5
        [Authorize(Roles = "Адміністратор")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var pare = await _context.Schedules.FindAsync(id);
            if (pare == null)
            {
                return NotFound();
            }
            ViewData["Pair_Time_Id"] = new SelectList(_context.PairTimes, "Id", "Id", pare.Pair_Time_Id);
            ViewData["Semester_Id"] = new SelectList(_context.Semesters, "Id", "Id", pare.Semester_Id);
            ViewData["Subject_Id"] = new SelectList(_context.Subject, "Id", "Id", pare.Subject_Id);
            ViewData["Teacher_Id"] = new SelectList(_context.Teachers, "Id", "Id", pare.Teacher_Id);
            return View(pare);
        }

        // POST: Pares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Адміністратор")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject_Id,Semester_Id,Teacher_Id,Pair_Time_Id,Week_Day")] Pare pare)
        {
            if (id != pare.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PareExists(pare.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pair_Time_Id"] = new SelectList(_context.PairTimes, "Id", "Id", pare.Pair_Time_Id);
            ViewData["Semester_Id"] = new SelectList(_context.Semesters, "Id", "Id", pare.Semester_Id);
            ViewData["Subject_Id"] = new SelectList(_context.Subject, "Id", "Id", pare.Subject_Id);
            ViewData["Teacher_Id"] = new SelectList(_context.Teachers, "Id", "Id", pare.Teacher_Id);
            return View(pare);
        }

        // GET: Pares/Delete/5
        [Authorize(Roles = "Адміністратор")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var pare = await _context.Schedules
                .Include(p => p.PairTime)
                .Include(p => p.Semester)
                .Include(p => p.Subject)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pare == null)
            {
                return NotFound();
            }

            return View(pare);
        }

        // POST: Pares/Delete/5
        [Authorize(Roles = "Адміністратор")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Schedules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Schedules'  is null.");
            }
            var pare = await _context.Schedules.FindAsync(id);
            if (pare != null)
            {
                _context.Schedules.Remove(pare);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PareExists(int id)
        {
          return (_context.Schedules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
