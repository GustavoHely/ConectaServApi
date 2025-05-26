using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class ContatoEmpresaCadastroDTO
    {
        [Required]
        public int EmpresaId { get; set; }

        [Required]
        public string TipoContato { get; set; } = string.Empty;

        [Required]
        public string Valor { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;
    }
}
