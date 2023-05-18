using System.ComponentModel.DataAnnotations;
namespace parcial.Models
{
    public class oferta
    {
        [Key]
        public int id_oferta { get; set; }
        public String? tipo_trabajo { get; set; }
        public String? experiencia { get; set; }
        public Double? salario { get; set; }
        public String? tipo_contrato { get; set; }
        public String? ubicacion { get; set; }
        public int? id_empresa { get; set; }
    }
}
