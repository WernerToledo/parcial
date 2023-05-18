using System.ComponentModel.DataAnnotations;
namespace parcial.Models
{
    public class comentario
    {
        [Key]
        public int id_det_oferta { get; set; }
        public String? Comentario { get; set; }
        public int? id_usuario_null { get; set; }
    }
}
