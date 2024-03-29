﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleProg.Data;
using ScheduleProg.Models;

namespace ScheduleProg.Controllers
{
    [Authorize(Roles = "Адміністратор")]
    public class PareSubgroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public PareSubgroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  IActionResult Index()
        {
            /*var applicationDbContext = _context.PareSubgroups.Include(p => p.Pare).Include(p => p.Subgroup);
            return View(await applicationDbContext.ToListAsync());*/
            return View();
        }
       
        public async Task<IActionResult> AdminView()
        {
            var applicationDbContext = _context.PareSubgroups.Include(p => p.Pare).Include(p => p.Subgroup);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: PareSubgroups

        /*public async Task<IActionResult> Index(string WeekDay)
        {
            *//*var applicationDbContext = _context.PareSubgroups.Include(p => p.Pare).Include(p => p.Subgroup);
            return View(await applicationDbContext.ToListAsync());*//*
            return Create(WeekDay);
        }*/

        // GET: PareSubgroups/Details/5
        
       

        public async Task<IActionResult> GetDay(string WeekDay) {

            return Create(WeekDay);
        }
        [HttpGet]
        // GET: PareSubgroups/Create
        public IActionResult Create(string Week_Day)
        {
            //var result =_context.Schedules.FromSqlRaw("Select * from Pare where Teacher_Id=2").ToList();
            ViewData["Pare_Id"] = new SelectList(_context.Schedules
                .Include(s => s.Subject)
                .Include(s => s.PairTime)
                .Where(s=>s.Week_Day== Week_Day)
                .Where(s=>s.Pair_Time_Id==1)
                , "Id", "Description");
            ViewData["Pare_Id2"] = new SelectList(_context.Schedules
               .Include(s => s.Subject)
               .Include(s => s.PairTime)
               .Where(s => s.Week_Day == Week_Day)
               .Where(s => s.Pair_Time_Id == 2)
               , "Id", "Description");
            ViewData["Pare_Id3"] = new SelectList(_context.Schedules
              .Include(s => s.Subject)
              .Include(s => s.PairTime)
              .Where(s => s.Week_Day == Week_Day)
              .Where(s => s.Pair_Time_Id == 3)
              , "Id", "Description");
            ViewData["Pare_Id4"] = new SelectList(_context.Schedules
              .Include(s => s.Subject)
              .Include(s => s.PairTime)
              .Where(s => s.Week_Day == Week_Day)
              .Where(s => s.Pair_Time_Id == 4)
              , "Id", "Description");
            ViewData["Pare_Id5"] = new SelectList(_context.Schedules
              .Include(s => s.Subject)
              .Include(s => s.PairTime)
              .Where(s => s.Week_Day == Week_Day)
              .Where(s => s.Pair_Time_Id == 5)
              , "Id", "Description");
            ViewData["Pare_Id6"] = new SelectList(_context.Schedules
              .Include(s => s.Subject)
              .Include(s => s.PairTime)
              .Where(s => s.Week_Day == Week_Day)
              .Where(s => s.Pair_Time_Id == 6)
              , "Id", "Description");

            ViewData["Subgroup_Id"] = new SelectList(_context.Subgroups, "Id", "Id");
            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int Pare_Id, 
            int Pare_Id2,
            int Pare_Id3, 
            int Pare_Id4,
            int Pare_Id5,
            int Pare_Id6,
            int Subgroup_Id)

            
        {
            PareSubgroup pareSubgroup = new PareSubgroup { Pare_Id=Pare_Id,Subgroup_Id=Subgroup_Id };
            PareSubgroup pareSubgroup2 = new PareSubgroup { Pare_Id = Pare_Id2, Subgroup_Id = Subgroup_Id };

            PareSubgroup pareSubgroup3 = new PareSubgroup { Pare_Id = Pare_Id3, Subgroup_Id = Subgroup_Id };
            PareSubgroup pareSubgroup4 = new PareSubgroup { Pare_Id = Pare_Id4, Subgroup_Id = Subgroup_Id };

            PareSubgroup pareSubgroup5= new PareSubgroup { Pare_Id = Pare_Id5, Subgroup_Id = Subgroup_Id };
            PareSubgroup pareSubgroup6 = new PareSubgroup { Pare_Id = Pare_Id6, Subgroup_Id = Subgroup_Id };

            if (ModelState.IsValid)
            {
                _context.Add(pareSubgroup);
                _context.Add(pareSubgroup2);
                _context.Add(pareSubgroup3);
                _context.Add(pareSubgroup4);
                _context.Add(pareSubgroup5);
                _context.Add(pareSubgroup6);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pare_Id"] = new SelectList(_context.Schedules, "Id", "Id", pareSubgroup.Pare_Id);
            ViewData["Pare_Id2"] = new SelectList(_context.Schedules, "Id", "Id", pareSubgroup2.Pare_Id);
            ViewData["Subgroup_Id"] = new SelectList(_context.Subgroups, "Id", "Id", pareSubgroup.Subgroup_Id);
            return View(pareSubgroup);
        }

        // GET: PareSubgroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PareSubgroups == null)
            {
                return NotFound();
            }

            var pareSubgroup = await _context.PareSubgroups.FindAsync(id);
            if (pareSubgroup == null)
            {
                return NotFound();
            }
            ViewData["Pare_Id"] = new SelectList(_context.Schedules, "Id", "Id", pareSubgroup.Pare_Id);
            ViewData["Subgroup_Id"] = new SelectList(_context.Subgroups, "Id", "Id", pareSubgroup.Subgroup_Id);
            return View(pareSubgroup);
        }

        // POST: PareSubgroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: PareSubgroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PareSubgroups == null)
            {
                return NotFound();
            }

            var pareSubgroup = await _context.PareSubgroups
                .Include(p => p.Pare)
                .Include(p => p.Subgroup)
                .FirstOrDefaultAsync(m => m.Pare_Id == id);
            if (pareSubgroup == null)
            {
                return NotFound();
            }

            return View(pareSubgroup);
        }

        // POST: PareSubgroups/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.PareSubgroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PareSubgroups'  is null.");
            }
            var pareSubgroup = _context.PareSubgroups.First(p=>p.Pare_Id == id);
            if (pareSubgroup != null)
            {
                _context.PareSubgroups.Remove(pareSubgroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        private bool PareSubgroupExists(int id)
        {
            return (_context.PareSubgroups?.Any(e => e.Pare_Id == id)).GetValueOrDefault();
        }
    }
}
