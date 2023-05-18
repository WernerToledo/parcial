using System.ComponentModel.DataAnnotations;
namespace parcial.Models
{
    public class det_oferta
    {
        [Key]
        public int? id_det_oferta { get; set; }
        public int? id_usuario { get; set; }
        public int? id_oferta { get; set; }
        public Byte[]? curriculum { get; set; }
    }
}
