using ArticleSystem.Entity;
using Microsoft.EntityFrameworkCore;

namespace ArticleSystem.Database
{
    public class ArticleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ArticleDbContext(DbContextOptions<ArticleDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(art =>
            {
                art.HasOne(x => x.Author).WithMany(x => x.Articles).HasForeignKey(x => x.AuthorId);
                art.HasMany(x => x.Tags).WithOne(x => x.Article).HasForeignKey(x => x.ArticleId);
                art.HasMany(x => x.Comments);
                art.Property(x => x.Date).HasDefaultValueSql("getdate()");
                art.HasIndex(x => x.Title).IsUnique();
            });

            modelBuilder.Entity<User>(usr =>
            {
                usr.HasMany(x => x.Comments);
                usr.HasOne(x => x.Role);

                usr.Property(x => x.RoleId).HasDefaultValue(1);
                usr.HasIndex(x => x.Login).IsUnique();
                usr.HasIndex(x => x.Email).IsUnique();
                
            });


            modelBuilder.Entity<Comment>(com =>
            {
                com.HasKey(x => new { x.Id, x.AuthorId, x.ArticleId });

                com.Property(x => x.Date).HasDefaultValueSql("getdate()");
                com.Property(x => x.ModyficationDate).HasDefaultValueSql("getdate()").ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<Tag>(tag =>
            {
                tag.HasOne(x => x.Article).WithMany(x => x.Tags).HasForeignKey(x => x.ArticleId);
            });


            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "User"},
                new Role() { Id = 2, Name = "PremiumUser" },
                new Role() { Id = 3, Name = "Moderator" },
                new Role() { Id = 4, Name = "Admin" }
            );
        }
    }
}
