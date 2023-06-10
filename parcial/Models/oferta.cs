using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
namespace parcial.Models
{
    public class oferta
    {
        [Key]
        public int id_oferta { get; set; }
        public String? tipo_trabajo { get; set; }//
        public String? experiencia { get; set; }//
        public Double? salario { get; set; }//
        public String? tipo_contrato { get; set; }//
        public String? ubicacion { get; set; }//
        public int? id_empresa { get; set; }
        public String? foto { get; set; }//
        public DateTime? fecha_publicacion { get; set; }//
        public String? fecha_contratacion { get; set; }//
        public int? estado { get; set; }

        public String? requisitos { get; set; }
        public String? habilidades { get; set; }
        public String? rango_edad { get; set; }
        public String? nivel_academico { get; set; }

    }
}
