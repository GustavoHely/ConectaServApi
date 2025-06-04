using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class EnderecoCadastroDTO
    {
        public string? Rua { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Bairro { get; set; }
        public string? Numero { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string CEP { get; set; } = string.Empty;

        public string? Complemento { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
