using Library_API_repeat.Api.Models;
using Microsoft.EntityFrameworkCore;
using AppUser = Library_API_repeat.Api.Models.User;

namespace Library_API_repeat.Api.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
            { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                   .HasIndex(u => u.Email)
                   .IsUnique();
        }
    }
}
