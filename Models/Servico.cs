using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class Servico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        // Se for null, significa que o valor será tratado como "sob consulta"
        public decimal? Preco { get; set; }

        // Indica se o serviço não tem preço fixo e será tratado sob consulta
        public bool PrecoSobConsulta { get; set; } = false;

        public bool Ativo { get; set; } = true;

        [Required]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; } = null!;
    }
}
