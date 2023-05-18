using System.ComponentModel.DataAnnotations;
namespace parcial.Models
{
    public class busquedas
    {
        [Key]
        public int id_busqueda { get; set; }
        public int? id_oferta { get; set; }
        public int? id_usuario { get; set; }
    }
}
