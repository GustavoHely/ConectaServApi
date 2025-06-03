using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class ContatoEmpresaCadastroDTO
    {
        [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "O tipo de contato é obrigatório.")]
        public string TipoContato { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor do contato é obrigatório.")]
        [RegularExpression(@"(^\d{8,11}$)|(^[\w\.\-]+@[\w\-]+\.[a-zA-Z]{2,}$)",
            ErrorMessage = "O valor deve ser um telefone (8-11 dígitos) ou um e-mail válido.")]
        public string Valor { get; set; } = string.Empty;
    }
}
