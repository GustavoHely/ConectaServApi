using System.ComponentModel.DataAnnotations;

public class ClienteCadastroDTO
{
    [Required]
    public string Telefone { get; set; } = string.Empty;

    [Required]
    public string Celular { get; set; } = string.Empty;

    [Required]
    public int EnderecoId { get; set; }

    public string FotoEstabelecimentoUrl { get; set; } = string.Empty;
}
