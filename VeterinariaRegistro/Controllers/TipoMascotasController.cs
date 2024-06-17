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
    public class TipoMascotasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoMascotasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoMascotas
        public async Task<IActionResult> Index()
        {
              return _context.TipoMascota != null ? 
                          View(await _context.TipoMascota.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TipoMascota'  is null.");
        }

        // GET: TipoMascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoMascota == null)
            {
                return NotFound();
            }

            var tipoMascota = await _context.TipoMascota
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoMascota == null)
            {
                return NotFound();
            }

            return View(tipoMascota);
        }

        // GET: TipoMascotas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMascotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,NombreTipo")] TipoMascota tipoMascota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoMascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMascota);
        }

        // GET: TipoMascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoMascota == null)
            {
                return NotFound();
            }

            var tipoMascota = await _context.TipoMascota.FindAsync(id);
            if (tipoMascota == null)
            {
                return NotFound();
            }
            return View(tipoMascota);
        }

        // POST: TipoMascotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,NombreTipo")] TipoMascota tipoMascota)
        {
            if (id != tipoMascota.IdTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMascotaExists(tipoMascota.IdTipo))
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
            return View(tipoMascota);
        }

        // GET: TipoMascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoMascota == null)
            {
                return NotFound();
            }

            var tipoMascota = await _context.TipoMascota
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoMascota == null)
            {
                return NotFound();
            }

            return View(tipoMascota);
        }

        // POST: TipoMascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoMascota == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TipoMascota'  is null.");
            }
            var tipoMascota = await _context.TipoMascota.FindAsync(id);
            if (tipoMascota != null)
            {
                _context.TipoMascota.Remove(tipoMascota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoMascotaExists(int id)
        {
          return (_context.TipoMascota?.Any(e => e.IdTipo == id)).GetValueOrDefault();
        }
    }
}
