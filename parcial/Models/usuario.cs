using System.ComponentModel.DataAnnotations;
namespace parcial.Models
{
    public class usuario
    {
        [Key]
        public int id_usuario { get; set; }
        public String? nombre { get; set; }
        public String nombre_usuario { get; set; }
        public int empresa { get; set; }
        public String? direccion { get; set; }
        public String? telefono { get; set; }
        //LA FOTO ES VARBINARY 
        public byte[]? foto { get; set; }
        public String? correo { get; set; }

        public String? password { get; set; }

    }
}
