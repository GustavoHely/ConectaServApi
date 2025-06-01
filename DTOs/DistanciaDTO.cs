using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    /// <summary>
    /// DTO para retornar a distância entre cliente e empresa
    /// </summary>
    public class DistanciaDTO
    {
        [Required]
        public int EmpresaId { get; set; }

        [Required]
        public string EmpresaNome { get; set; } = string.Empty;

        [Required]
        public double DistanciaKm { get; set; }
    }
}
