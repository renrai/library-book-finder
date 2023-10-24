using Microsoft.EntityFrameworkCore;
using ProjectChallengeData.Database.Entities;
using ProjectLibraryData.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeData.Database
{
    public class ProjectContextDb : DbContext
    {
        public ProjectContextDb(DbContextOptions<ProjectContextDb> options) : base(options)
        {

        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>(build =>
            {
                build.ToTable("Books");
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.BookName).HasMaxLength(120);
                build.Property(entry => entry.Shelf).HasMaxLength(350);
                build.Property(entry => entry.Author).HasMaxLength(120);
            });
            modelBuilder.Entity<UserEntity>(build =>
            {
                build.ToTable("Users");
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Login).HasMaxLength(43);
                build.Property(entry => entry.UserName).HasMaxLength(21);
                build.Property(entry => entry.Password).HasMaxLength(120);
            });
        }
    }
}
