using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class EmpresaCadastroDTO
    {
        public int? Id { get; set; }

        [Required]
        public int PrestadorId { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter exatamente 14 dígitos numéricos.")]
        public string Cnpj { get; set; } = string.Empty;


        public string FotoEstabelecimentoUrl { get; set; } = string.Empty;

        public int? EnderecoId { get; set; }
    }
}
