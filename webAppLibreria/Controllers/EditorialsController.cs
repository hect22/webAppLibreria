﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webAppLibreria.Models;

namespace webAppLibreria.Controllers
{
    public class EditorialsController : Controller
    {
        private readonly EditorialContext _context;

        public EditorialsController(EditorialContext context)
        {
            _context = context;
        }

        // GET: Editorials
        public async Task<IActionResult> Index()
        {
            return View(await _context.Editorials.ToListAsync());
        }

        // GET: Editorials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editorials
                .FirstOrDefaultAsync(m => m.Ideditorial == id);
            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // GET: Editorials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editorials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ideditorial,Editorial1,NombreContacto,Direccion,Ciudad,Telefono,Email,Comentario")] Editorial editorial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editorial);
        }

        // GET: Editorials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editorials.FindAsync(id);
            if (editorial == null)
            {
                return NotFound();
            }
            return View(editorial);
        }

        // POST: Editorials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ideditorial,Editorial1,NombreContacto,Direccion,Ciudad,Telefono,Email,Comentario")] Editorial editorial)
        {
            if (id != editorial.Ideditorial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorialExists(editorial.Ideditorial))
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
            return View(editorial);
        }

        // GET: Editorials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editorials
                .FirstOrDefaultAsync(m => m.Ideditorial == id);
            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // POST: Editorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editorial = await _context.Editorials.FindAsync(id);
            if (editorial != null)
            {
                _context.Editorials.Remove(editorial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorialExists(int id)
        {
            return _context.Editorials.Any(e => e.Ideditorial == id);
        }
    }
}
