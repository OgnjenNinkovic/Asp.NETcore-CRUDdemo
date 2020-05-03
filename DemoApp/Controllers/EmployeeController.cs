using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DemoApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeesContext _context;

        public EmployeeController(EmployeesContext context)
        {
            _context = context;
        }

       // GET: Employee
       [Authorize]
       [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(string SearchValue)
        {

            var q = from e in _context.Employees select e;

            if (!string.IsNullOrEmpty(SearchValue))
            {
                q = q.Where(e => e.FullName.Contains(SearchValue) || e.EmpCode.Contains(SearchValue));
            }


            //if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //    return View(await q.AsNoTracking().ToListAsync());

            return PartialView("SearchPartial", q.ToList());

        }


        // GET: Employee/Create
        public IActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new Employee());
            }
            else
            {
                return View(_context.Employees.Find(id));

            }
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,FullName,EmpCode,Position,OfficeLocation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if(employee.EmployeeId == 0)
                {
                    _context.Add(employee);
                }
                else
                {
                    _context.Update(employee);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

      
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
