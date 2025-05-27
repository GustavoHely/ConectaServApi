using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class ServicoCadastroDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        // Preço pode ser nulo se for sob consulta
        public decimal? Preco { get; set; }

        public bool PrecoSobConsulta { get; set; } = false;

        public bool Ativo { get; set; } = true;

        [Required]
        public int EmpresaId { get; set; }
    }
}
