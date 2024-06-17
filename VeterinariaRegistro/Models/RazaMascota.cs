using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeterinariaRegistro.Models
{
    public class RazaMascota
    {
        [Key]
        public int IdRaza { get; set; }

        public string NombreRaza { get; set; }
        
        public int IdTipoMascota { get; set; }
        public TipoMascota TipoMascota { get; set; }
       
        public ICollection<Mascota> Mascotas { get; set; }
    }
}
