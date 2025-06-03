using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Prestador Prestador { get; set; } = new Prestador();

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required]
        [MaxLength(14)]
        public string Cnpj { get; set; } = string.Empty;


        public string FotoEstabelecimentoUrl { get; set; } = string.Empty;

        public int? EnderecoId { get; set; }

        [ForeignKey("EnderecoId")]
        public Endereco? Endereco { get; set; }

        public List<ContatoEmpresa> Contatos { get; set; } = new();
    }
}
