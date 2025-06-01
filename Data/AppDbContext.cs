using Microsoft.EntityFrameworkCore;
using ConectaServApi.Models;

namespace ConectaServApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TesteTemporario> Testes { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<ContatoEmpresa> ContatosEmpresa { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<PlanoAssinatura> PlanosAssinatura { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<PagamentoAssinatura> PagamentosAssinatura { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<AvaliacaoCliente> AvaliacoesCliente { get; set; }
        public DbSet<AvaliacaoPrestador> AvaliacoesPrestador { get; set; }
        public DbSet<EstatisticasPrestador> EstatisticasPrestador { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Denuncia> Denuncias { get; set; }

    }
}