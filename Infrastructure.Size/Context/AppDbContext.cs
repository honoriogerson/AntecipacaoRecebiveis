using Domain.Size.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Size.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
