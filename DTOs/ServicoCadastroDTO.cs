using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class ServicoCadastroDTO
    {
        /// <summary>
        /// Nome do serviço oferecido. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada do serviço. Obrigatória.
        /// </summary>
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Preço do serviço. Opcional caso o serviço seja sob consulta.
        /// </summary>
        public decimal? Preco { get; set; }

        /// <summary>
        /// Indica se o preço está marcado como sob consulta.
        /// </summary>
        public bool PrecoSobConsulta { get; set; }

        /// <summary>
        /// Indica se o serviço está ativo. Obrigatório.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// ID da empresa responsável pelo serviço. Obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
        public int EmpresaId { get; set; }
    }
}
