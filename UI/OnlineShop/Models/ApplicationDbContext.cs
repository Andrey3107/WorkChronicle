namespace OnlineShop.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Product { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("Product").HasKey(p => p.Id);
            modelBuilder.Entity<Orders>()
                .ToTable("Orders").HasKey(p => p.OrderId);

            modelBuilder.Entity<User>().HasNoDiscriminator()
                .ToTable("AspNetUsers").HasKey(p => p.Id);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoDiscriminator()
                .ToTable("AspNetUserLogins").HasKey(p => new { p.LoginProvider, p.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoDiscriminator()
                .ToTable("AspNetUserRoles").HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoDiscriminator()
                .ToTable("AspNetUserTokens").HasKey(p => new { p.UserId, p.LoginProvider, p.Name });
            modelBuilder.Entity<IdentityUserClaim<string>>().HasNoDiscriminator()
                .ToTable("AspNetUserClaims").HasKey(p => p.Id);
            modelBuilder.Entity<IdentityRole>().HasNoDiscriminator()
                .ToTable("AspNetRoles").HasKey(p => p.Id);
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasNoDiscriminator()
                .ToTable("AspNetRoleClaims").HasKey(p => p.Id);
        }
    }
}
