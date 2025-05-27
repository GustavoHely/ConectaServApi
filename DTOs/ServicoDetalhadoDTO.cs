namespace ConectaServApi.DTOs
{
    public class ServicoDetalhadoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal? Preco { get; set; }
        public bool PrecoSobConsulta { get; set; }
        public bool Ativo { get; set; }

        // Dados da Empresa
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string? FotoEstabelecimentoUrl { get; set; }

        // Endereço
        public string EnderecoCompleto { get; set; } = string.Empty;

        // Contatos
        public List<string> Contatos { get; set; } = new();
    }
}
