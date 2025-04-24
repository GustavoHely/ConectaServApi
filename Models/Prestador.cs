using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class Prestador
    {
        public int Id { get; set; }

        [Required]
        public string Cnpj { get; set; }

        [Required]
        public string NomeFantasia { get; set; }

        public string? RazaoSocial { get; set; }
        public string? Telefone { get; set; }

        [Required]
        public string Celular { get; set; }

        public string? Site { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
