using api_flutter.Models;
using Microsoft.EntityFrameworkCore;

namespace api_flutter.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tarefa>()
                .HasKey(t => t.Titulo);

            builder.Entity<Journal>()
                .HasKey(j => j.Id);
        }

        public DbSet<Tarefa> Tarefas { get; set; } = default!;
        public DbSet<Journal> Journals { get; set; } = default!;
    }
}