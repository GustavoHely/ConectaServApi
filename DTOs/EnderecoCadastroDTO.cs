using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    /// <summary>
    /// DTO de cadastro de endereço. Apenas o CEP é obrigatório.
    /// Os demais campos são preenchidos automaticamente via Google Maps.
    /// </summary>
    public class EnderecoCadastroDTO
    {
        /// <summary>
        /// Rua (preenchida automaticamente pela API do Google)
        /// </summary>
        public string? Rua { get; set; }

        /// <summary>
        /// Estado do endereço (preenchido automaticamente)
        /// </summary>
        public string Estado { get; set; } = string.Empty;

        /// <summary>
        /// Cidade do endereço (preenchida automaticamente)
        /// </summary>
        public string Cidade { get; set; } = string.Empty;

        /// <summary>
        /// Bairro do endereço (preenchido automaticamente)
        /// </summary>
        public string Bairro { get; set; } = string.Empty;

        /// <summary>
        /// Número do imóvel (opcional, algumas regiões como Brasília não utilizam)
        /// </summary>
        public int? Numero { get; set; }

        /// <summary>
        /// CEP do endereço (obrigatório)
        /// </summary>
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string CEP { get; set; } = string.Empty;

        /// <summary>
        /// Complemento (ex: bloco, apto, lote) — opcional
        /// </summary>
        public string? Complemento { get; set; }

        /// <summary>
        /// Latitude (preenchida automaticamente via Google)
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Longitude (preenchida automaticamente via Google)
        /// </summary>
        public double? Longitude { get; set; }
    }
}
