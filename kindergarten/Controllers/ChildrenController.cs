#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kindergarten.Models;
using Microsoft.AspNetCore.Authorization;


namespace kindergarten.Controllers
{
    [Authorize]
    public class ChildrenController : Controller
    {
        private readonly kindergartenDbContext _context;

        public ChildrenController(kindergartenDbContext context)
        {
            _context = context;
        }

        // GET: Children
        //public async Task<IActionResult> Index()
        //{
        //    var kindergartenDbContext = _context.Children.Include(c => c.Parent);
        //    return View(await kindergartenDbContext.ToListAsync());
        //}
        [Authorize]
        // GET: Children
        public IActionResult Index(string sortOrder, string searchString, int pg = 1)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "ChildId" ? "ChildId_desc" : "ChildId";
            var children = from c in _context.Children
                           select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                children = children.Where(c => c.LastName.Contains(searchString)
                                       || c.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    children = children.OrderByDescending(c => c.LastName);
                    break;
                case "ChildId":
                    children = children.OrderBy(c => c.ChildId);
                    break;
                case "ChildId_desc":
                    children = children.OrderByDescending(c => c.ChildId);
                    break;
                default:
                    children = children.OrderBy(c => c.LastName);
                    break;
            }
            //return View(children.ToList());

            const int pageSize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = children.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = children.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.ChildId == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildId,FirstName,LastName,Age,Gender,Allegies,AllergyDescription,ParentId,MonthlyFees")] Child child)
        {
            if (ModelState.IsValid)
            {
                _context.Add(child);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", child.ParentId);
            return View(child);
        }

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", child.ParentId);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChildId,FirstName,LastName,Age,Gender,Allegies,AllergyDescription,ParentId,MonthlyFees")] Child child)
        {
            if (id != child.ChildId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(child);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildExists(child.ChildId))
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
            ViewData["ParentId"] = new SelectList(_context.Parents, "ParentId", "ParentId", child.ParentId);
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.ChildId == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var child = await _context.Children.FindAsync(id);
            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildExists(int id)
        {
            return _context.Children.Any(e => e.ChildId == id);
        }
    }
}
