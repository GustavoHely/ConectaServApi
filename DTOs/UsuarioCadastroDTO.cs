using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class UsuarioCadastroDTO
    {
        /// <summary>
        /// Nome completo do usuário. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Endereço de e-mail válido. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha de acesso ao sistema. Obrigatória.
        /// </summary>
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; } = string.Empty;

        /// <summary>
        /// CPF do usuário com exatamente 11 dígitos numéricos. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos numéricos.")]
        public string CPF { get; set; } = string.Empty;

        /// <summary>
        /// Número de telefone fixo do usuário. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; } = string.Empty;

        /// <summary>
        /// Número de celular do usuário. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O celular é obrigatório.")]
        public string Celular { get; set; } = string.Empty;

        /// <summary>
        /// URL da foto de perfil do usuário. Opcional.
        /// </summary>
        public string FotoPerfilUrl { get; set; } = string.Empty;

        /// <summary>
        /// ID do endereço associado ao usuário. Opcional.
        /// </summary>
        public int? EnderecoId { get; set; }
    }
}
