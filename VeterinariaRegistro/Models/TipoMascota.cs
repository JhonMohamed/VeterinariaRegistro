using System.ComponentModel.DataAnnotations;

namespace VeterinariaRegistro.Models
{
    public class TipoMascota
    {
        [Key]
        public int IdTipo { get; set; }

        public string NombreTipo { get; set; }

        public ICollection<RazaMascota> RazasMascotas { get; set; }
    }
}
