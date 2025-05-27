using ConectaServApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EnderecoId { get; set; }

    [ForeignKey("EnderecoId")]
    public Endereco Endereco { get; set; } = new Endereco();

    [Required]
    [ForeignKey("UsuarioId")]
    public int UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = new Usuario();
}
