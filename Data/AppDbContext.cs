using api_flutter.Models;
using Microsoft.EntityFrameworkCore;

namespace api_flutter.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; } = default!;
        public DbSet<Journal> Journals { get; set; } = default!;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:vangeanc3.database.windows.net,1433;Initial Catalog=vangeanceAppDb;Persist Security Info=False;User ID=vangeanc3;Password=Gmmi080605.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tarefa>()
                .HasKey(t => t.Titulo);

            builder.Entity<Journal>()
                .HasKey(j => j.Id);
        }
    }
}