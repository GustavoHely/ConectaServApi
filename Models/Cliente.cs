using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        public string Celular { get; set; } = string.Empty;

        [Required]
        public int EnderecoId { get; set; }

        public string FotoEstabelecimentoUrl { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}

