using Microsoft.EntityFrameworkCore;
using GSMicroServices.Model;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GSMicroServices.Data
{
    public class PromptContext : DbContext
    {
        public DbSet<Prompt> Prompt { get; set; }

        public PromptContext(DbContextOptions<PromptContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseMySql(
                    "server=localhost;database=promptdb;user=root;password=123;",
                    new MySqlServerVersion(new Version(8, 0, 36))
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prompt>().ToTable("Prompt");
        }
    }
}
