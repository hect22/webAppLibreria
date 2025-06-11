using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webAppLibreria.Models;
using Microsoft.AspNetCore.Http;

namespace webAppLibreria.Controllers
{
    public class LibroesController : Controller
    {
        private readonly EditorialContext _context;

        public LibroesController(EditorialContext context)
        {
            _context = context;
        }

        // GET: Libroes
        public async Task<IActionResult> Index()
        {
            var editorialContext = _context.Libros.Include(l => l.IdeditorialNavigation);
            return View(await editorialContext.ToListAsync());
        }

        // GET: Libroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros
                .Include(l => l.IdeditorialNavigation)
                .FirstOrDefaultAsync(m => m.Idlibro == id);
            if (libro == null) return NotFound();

            return View(libro);
        }

        // GET: Libroes/Create
        public IActionResult Create()
        {
            ViewData["Ideditorial"] = new SelectList(_context.Editorials, "Ideditorial", "Ideditorial");
            return View();
        }

        // POST: Libroes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (await _context.Libros.AnyAsync(l => l.Isbn == libro.Isbn))
            {
                ModelState.AddModelError("Isbn", "Ya existe un libro con ese ISBN.");
                ViewData["Ideditorial"] = new SelectList(_context.Editorials, "Ideditorial", "Ideditorial", libro.Ideditorial);
                return View(libro);
            }

            // Procesar la imagen si se sube
            if (libro.FotoArchivo != null && libro.FotoArchivo.Length > 0)
            {
                using var ms = new MemoryStream();
                await libro.FotoArchivo.CopyToAsync(ms);
                libro.Foto = ms.ToArray();
            }

            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Ideditorial"] = new SelectList(_context.Editorials, "Ideditorial", "Ideditorial", libro.Ideditorial);
            return View(libro);
        }

        // GET: Libroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound();

            ViewData["Ideditorial"] = new SelectList(_context.Editorials, "Ideditorial", "Ideditorial", libro.Ideditorial);
            return View(libro);
        }

        // POST: Libroes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            if (id != libro.Idlibro) return NotFound();

            var libroExistente = await _context.Libros.FindAsync(id);
            if (libroExistente == null) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    libroExistente.Isbn = libro.Isbn;
                    libroExistente.Titulo = libro.Titulo;
                    libroExistente.Autor = libro.Autor;
                    libroExistente.Año = libro.Año;
                    libroExistente.Precio = libro.Precio;
                    libroExistente.Comentarios = libro.Comentarios;
                    libroExistente.Ideditorial = libro.Ideditorial;

                    if (libro.FotoArchivo != null && libro.FotoArchivo.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await libro.FotoArchivo.CopyToAsync(ms);
                        libroExistente.Foto = ms.ToArray();
                    }

                    _context.Update(libroExistente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Idlibro)) return NotFound();
                    else throw;
                }
            }

            ViewData["Ideditorial"] = new SelectList(_context.Editorials, "Ideditorial", "Ideditorial", libro.Ideditorial);
            return View(libro);
        }

        // GET: Libroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros
                .Include(l => l.IdeditorialNavigation)
                .FirstOrDefaultAsync(m => m.Idlibro == id);
            if (libro == null) return NotFound();

            return View(libro);
        }

        // POST: Libroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Mostrar imagen del libro
        public async Task<IActionResult> MostrarImagen(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null || libro.Foto == null)
            {
                return NotFound();
            }

            return File(libro.Foto, "img/jpeg");
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Idlibro == id);
        }
    }
}
