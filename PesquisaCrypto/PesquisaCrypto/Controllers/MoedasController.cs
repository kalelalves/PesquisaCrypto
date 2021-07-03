using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PesquisaCrypto.Models;

namespace PesquisaCrypto.Controllers
{
    public class MoedasController : Controller
    {
        private readonly MoedasContexto _context;

        public MoedasController(MoedasContexto context)
        {
            _context = context;
        }

        // GET: Moedas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Moedas.ToListAsync());
        }

        

        // GET: Moedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moedas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoedasId,Nome,Quantidade")] Moedas moedas)
        {
            if (ModelState.IsValid)
            {
                moedas.Quantidade = 0;
                _context.Add(moedas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moedas);
        }

        // GET: Moedas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moedas = await _context.Moedas.FindAsync(id);
            if (moedas == null)
            {
                return NotFound();
            }
            return View(moedas);
        }

        // POST: Moedas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoedasId,Nome,Quantidade")] Moedas moedas)
        {
            if (id != moedas.MoedasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moedas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoedasExists(moedas.MoedasId))
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
            return View(moedas);
        }

        // GET: Moedas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moedas = await _context.Moedas
                .FirstOrDefaultAsync(m => m.MoedasId == id);
            if (moedas == null)
            {
                return NotFound();
            }

            return View(moedas);
        }

        // POST: Moedas/Delete/5


        private bool MoedasExists(int id)
        {
            return _context.Moedas.Any(e => e.MoedasId == id);
        }
    }
}
