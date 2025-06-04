using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Estado { get; set; } = string.Empty;

        public string Cidade { get; set; } = string.Empty;

        public string Bairro { get; set; } = string.Empty;

        [Required]
        public string Rua { get; set; } = string.Empty;

        [Required]
        public string Numero { get; set; } = string.Empty;

        [Required]
        public string CEP { get; set; } = string.Empty;

        public string Complemento { get; set; } = string.Empty;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
