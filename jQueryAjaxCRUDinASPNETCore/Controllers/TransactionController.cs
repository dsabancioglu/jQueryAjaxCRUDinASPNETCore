using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jQueryAjaxCRUDinASPNETCore.Entities;
using jQueryAjaxCRUDinASPNETCore.Models;
using static jQueryAjaxCRUDinASPNETCore.Helper;

namespace jQueryAjaxCRUDinASPNETCore.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionManagementContext _context;

        public TransactionController(TransactionManagementContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            id = 0;
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // GET: Transaction/Create
        [NoDirectAccess] //arama çubuğundan adresi yazınca popup çalıştırmadan direkt view döndürmesini engelledik
        public IActionResult Create()
        {
            return View(new TransactionModel());
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                transactionModel.Date = DateTime.Now;
                _context.Add(transactionModel);
                await _context.SaveChangesAsync();

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this,"_ViewAll",_context.Transactions.ToList())});
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", transactionModel)});
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions.FindAsync(id);
            if (transactionModel == null)
            {
                return NotFound();
            }
            return View(transactionModel);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amonut")] TransactionModel transactionModel)
        {
            if(id != transactionModel.TransactionId || !TransactionModelExists(transactionModel.TransactionId))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Transactions.Update(transactionModel);
                    await _context.SaveChangesAsync(); /*Success = true, SuccessMessage = "Well updated successfully" dene isValid yerine*/
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", transactionModel) });
                }
            }
            ModelState.AddModelError("Error", "Model is not valid"); //modelstate i nasıl gönderirim araştır
            return Json(new { Success = false, ModelState = ModelState, isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", transactionModel) });
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transactionModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
        }

        private bool TransactionModelExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}

//custom exception handler middleware
//weell, configuration,entity
//Modelstate olayını araştır
//login ekle
//mapper