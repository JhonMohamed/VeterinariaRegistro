using System;
using System.Collections.Generic;
using System.IO; // Agregar este using para trabajar con archivos
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinariaRegistro.Datos;
using VeterinariaRegistro.Models;
using VeterinariaRegistro.ViewModels;

namespace VeterinariaRegistro.Controllers
{
    public class MascotasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _entorno;

        public MascotasController(ApplicationDbContext context, IWebHostEnvironment entorno)
        {
            _context = context;
            _entorno = entorno;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index()
        {
            var mascotas = await _context.Mascota
                .Include(m => m.RazaMascota)
                    .ThenInclude(r => r.TipoMascota)
                .ToListAsync();
            var tiposMascotas = await _context.TipoMascota.ToListAsync();
            var razasMascotas = await _context.RazaMascota.ToListAsync();

            ViewBag.TiposMascotas = tiposMascotas;
            ViewBag.RazasMascotas = razasMascotas;

            return View(mascotas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mascota == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascota
                .Include(m => m.RazaMascota)
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // GET: Mascotas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MascotaVM mascotaVM)
        {
            if (mascotaVM.ImagenFile == null)
            {
                ModelState.AddModelError("ImagenFile", "El archivo de imagen es obligatorio");
            }

            if (!ModelState.IsValid)
            {
                return View(mascotaVM);
            }

            try
            {
                string NuevoNombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(mascotaVM.ImagenFile.FileName);
                string rutaCompletaImagen = Path.Combine(_entorno.WebRootPath, "imagenes", NuevoNombreArchivo);

                using (var stream = new FileStream(rutaCompletaImagen, FileMode.Create))
                {
                    mascotaVM.ImagenFile.CopyTo(stream);
                }

                Mascota m = new Mascota
                {
                    NombreMascota = mascotaVM.NombreMascota,
                    NombrePropietario = mascotaVM.NombrePropietario,
                    color = mascotaVM.Color,
                    Fecha = DateTime.Now,
                    estado = mascotaVM.Estado,
                    ImagenMascota = NuevoNombreArchivo
                };

                _context.Mascota.Add(m);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar la mascota: " + ex.Message);
                return View(mascotaVM);
            }
        }

        public JsonResult ObtenerRazas(int idTipoMascota)
        {
            var razas = _context.RazaMascota
                .Where(r => r.IdTipoMascota == idTipoMascota)
                .Select(r => new { IdRaza = r.IdRaza, NombreRaza = r.NombreRaza })
                .ToList();

            return Json(razas);
        }

        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mascota == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascota.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            ViewData["IdRazaMascota"] = new SelectList(_context.RazaMascota, "IdRaza", "IdRaza", mascota.IdRazaMascota);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mascota == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascota
                .Include(m => m.RazaMascota)
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mascota == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mascota' is null.");
            }

            var mascota = await _context.Mascota.FindAsync(id);
            if (mascota != null)
            {
                _context.Mascota.Remove(mascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
            return _context.Mascota?.Any(e => e.IdMascota == id) ?? false;
        }
    }
}