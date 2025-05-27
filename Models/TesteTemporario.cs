using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.Models
{
    public class TesteTemporario
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
    }
}
