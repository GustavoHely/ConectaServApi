using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class ContatoEmpresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; } = new Empresa();

        [Required]
        public string TipoContato { get; set; } = string.Empty;

        [Required]
        public string Valor { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;
    }
}
