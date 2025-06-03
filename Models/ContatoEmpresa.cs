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
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }

        public Empresa Empresa { get; set; } = new Empresa();

        [Required]
        [MaxLength(20)]
        public string TipoContato { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Valor { get; set; } = string.Empty;
    }
}
