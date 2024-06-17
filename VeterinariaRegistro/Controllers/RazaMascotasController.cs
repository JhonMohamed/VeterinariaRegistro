using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinariaRegistro.Datos;
using VeterinariaRegistro.Models;

namespace VeterinariaRegistro.Controllers
{
    public class RazaMascotasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RazaMascotasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RazaMascotas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RazaMascota.Include(r => r.TipoMascota);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RazaMascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RazaMascota == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazaMascota
                .Include(r => r.TipoMascota)
                .FirstOrDefaultAsync(m => m.IdRaza == id);
            if (razaMascota == null)
            {
                return NotFound();
            }

            return View(razaMascota);
        }

        // GET: RazaMascotas/Create
        public IActionResult Create()
        {
            ViewData["IdTipoMascota"] = new SelectList(_context.TipoMascota, "IdTipo", "IdTipo");
            return View();
        }

        // POST: RazaMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRaza,NombreRaza,IdTipoMascota")] RazaMascota razaMascota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(razaMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoMascota"] = new SelectList(_context.TipoMascota, "IdTipo", "IdTipo", razaMascota.IdTipoMascota);
            return View(razaMascota);
        }

        // GET: RazaMascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RazaMascota == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazaMascota.FindAsync(id);
            if (razaMascota == null)
            {
                return NotFound();
            }
            ViewData["IdTipoMascota"] = new SelectList(_context.TipoMascota, "IdTipo", "IdTipo", razaMascota.IdTipoMascota);
            return View(razaMascota);
        }

        // POST: RazaMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRaza,NombreRaza,IdTipoMascota")] RazaMascota razaMascota)
        {
            if (id != razaMascota.IdRaza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(razaMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RazaMascotaExists(razaMascota.IdRaza))
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
            ViewData["IdTipoMascota"] = new SelectList(_context.TipoMascota, "IdTipo", "IdTipo", razaMascota.IdTipoMascota);
            return View(razaMascota);
        }

        // GET: RazaMascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RazaMascota == null)
            {
                return NotFound();
            }

            var razaMascota = await _context.RazaMascota
                .Include(r => r.TipoMascota)
                .FirstOrDefaultAsync(m => m.IdRaza == id);
            if (razaMascota == null)
            {
                return NotFound();
            }

            return View(razaMascota);
        }

        // POST: RazaMascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RazaMascota == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RazaMascota'  is null.");
            }
            var razaMascota = await _context.RazaMascota.FindAsync(id);
            if (razaMascota != null)
            {
                _context.RazaMascota.Remove(razaMascota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RazaMascotaExists(int id)
        {
          return (_context.RazaMascota?.Any(e => e.IdRaza == id)).GetValueOrDefault();
        }
    }
}
