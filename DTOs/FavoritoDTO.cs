using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class FavoritoDTO
    {
        public int? Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int PrestadorId { get; set; }
    }
}
