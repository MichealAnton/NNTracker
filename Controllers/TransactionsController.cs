using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NNTracker.Data;
using NNTracker.Models;

namespace NNTracker.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDBcontext _context;

        public TransactionsController(ApplicationDBcontext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var applicationDBcontext = _context.Transactions.Include(t => t.Account).Include(t => t.Category);
            return View(await applicationDBcontext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Transaction_Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["Account_Id"] = new SelectList(_context.Accounts, "Account_Id", "Account_Name");
            ViewData["Category_Id"] = new SelectList(_context.Categories, "Category_Id", "Category_Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Transaction_Id,Transaction_Value,Date,Transaction_Notes,Account_Id,Category_Id")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Account_Id"] = new SelectList(_context.Accounts, "Account_Id", "Account_Name", transaction.Account_Id);
            ViewData["Category_Id"] = new SelectList(_context.Categories, "Category_Id", "Category_Name", transaction.Category_Id);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["Account_Id"] = new SelectList(_context.Accounts, "Account_Id", "Account_Name", transaction.Account_Id);
            ViewData["Category_Id"] = new SelectList(_context.Categories, "Category_Id", "Category_Name", transaction.Category_Id);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Transaction_Id,Transaction_Value,Date,Transaction_Notes,Account_Id,Category_Id")] Transaction transaction)
        {
            if (id != transaction.Transaction_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Transaction_Id))
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
            ViewData["Account_Id"] = new SelectList(_context.Accounts, "Account_Id", "Account_Name", transaction.Account_Id);
            ViewData["Category_Id"] = new SelectList(_context.Categories, "Category_Id", "Category_Name", transaction.Category_Id);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Transaction_Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Transaction_Id == id);
        }
    }
}
