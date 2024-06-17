using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaRegistro.Models
{
    public class Mascota
    {
        [Key]
        public int IdMascota { get; set; }

       
        public string NombreMascota { get; set; }
        public string NombrePropietario { get; set; }
        public DateTime Fecha { get; set; }
        public bool estado { get; set; }
        public string color {  get; set; }


        public string ImagenMascota { get; set; }

       
        public int IdRazaMascota { get; set; }
      
        public RazaMascota? RazaMascota { get; set; }
    }
}
