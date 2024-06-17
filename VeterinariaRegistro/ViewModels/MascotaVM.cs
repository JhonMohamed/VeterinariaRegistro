using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VeterinariaRegistro.Models;

namespace VeterinariaRegistro.ViewModels
{
    public class MascotaVM
    {
        [Required]
            public string NombreMascota { get; set; }
        [Required]
            public string NombrePropietario { get; set; }
            public bool Estado { get; set; }
            public string Color { get; set; }
            public IFormFile ImagenFile { get; set; }
            public int IdTipoMascota { get; set; }
            public int IdRazaMascota { get; set; }

            // Propiedades para los dropdowns
            public List<SelectListItem> TiposMascotas { get; set; }
            public List<SelectListItem> RazasMascotas { get; set; }

    }
}
