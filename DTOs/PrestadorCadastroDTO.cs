using System.ComponentModel.DataAnnotations;

public class PrestadorCadastroDTO
{
    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;

    [Required]
    public string NumTelefone { get; set; } = string.Empty;

    public string FotoPerfil { get; set; } = string.Empty;
    public string? Cpf { get; set; } = string.Empty;

    [Required]
    public string Cnpj { get; set; } = string.Empty;

    [Required]
    public string RazaoSocial { get; set; } = string.Empty;

    [Required]
    public string Telefone { get; set; } = string.Empty;

    [Required]
    public string Celular { get; set; } = string.Empty;

    [Required]
    public int EnderecoId { get; set; }

    public string FotoEstabelecimentoUrl { get; set; } = string.Empty;
}
