using Loja;
using Loja.Models;
using Microsoft.EntityFrameworkCore;


namespace loja.Data
{
    public class LojaDbContext : DbContext
    {
        public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedores> Fornecedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}

