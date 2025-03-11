using LibrariesWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LibrariesWeb.Persistance.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibrariesWeb.Persistance.Data;

public class LibraryContext: IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        :base(options)
    { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<IssuedBook> IssuedBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityUserConfiguration).Assembly);
    }
}
