using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConectaServApi.Models;

namespace ConectaServApi.Models
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PrestadorId { get; set; }

        [ForeignKey("PrestadorId")]
        public Prestador Prestador { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required]
        public string Cnpj { get; set; } = string.Empty;

        public List<ContatoEmpresa> Contatos { get; set; } = new();
    }
}
