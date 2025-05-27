namespace ConectaServApi.DTOs
{
    public class CartaoDetalhadoDTO
    {
        public int Id { get; set; }
        public string NomeTitular { get; set; } = string.Empty;

        // Apenas os 4 últimos dígitos do número
        public string FinalCartao { get; set; } = string.Empty;

        public DateTime Validade { get; set; }

        public int PrestadorId { get; set; }
    }
}
