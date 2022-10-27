using Microsoft.EntityFrameworkCore;
using System;

namespace KarimiProject.SqlService
{
    public class MainDbContext:DbContext
    {
        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=.;Database=KarimiDataBase;User Id=sa;Password=Karimi@123");
        }

        public DbSet<Entities.CustomerDbModel> Customers { get; set; }
        public DbSet<Entities.ImportedFile> ImportedFiles { get; set; }
    }
}
