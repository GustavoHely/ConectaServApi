using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string CPF { get; set; }

        public string Telefone { get; set; }
        public string Celular { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
