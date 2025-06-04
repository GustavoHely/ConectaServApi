using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class EmpresaCadastroDTO
    {
        /// <summary>
        /// ID da empresa (usado em edições ou respostas). Obrigatório se necessário no controller.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome fantasia da empresa. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Razão social da empresa. Obrigatória.
        /// </summary>
        [Required(ErrorMessage = "A razão social é obrigatória.")]
        public string RazaoSocial { get; set; } = string.Empty;

        /// <summary>
        /// CNPJ da empresa com 14 dígitos numéricos. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter exatamente 14 dígitos numéricos.")]
        public string Cnpj { get; set; } = string.Empty;

        /// <summary>
        /// URL da foto do estabelecimento. Opcional.
        /// </summary>
        public string FotoEstabelecimentoUrl { get; set; } = string.Empty;

        /// <summary>
        /// ID do prestador associado à empresa. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O ID do prestador é obrigatório.")]
        public int PrestadorId { get; set; }
    }
}
