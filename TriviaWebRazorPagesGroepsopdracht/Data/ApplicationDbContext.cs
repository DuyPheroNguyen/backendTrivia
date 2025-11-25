using Microsoft.EntityFrameworkCore;
using TriviaWebRazorPages.Models;

namespace TriviaWebRazorPages.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Choice> Choices { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("category");  // let op kleine letters
            modelBuilder.Entity<Question>().ToTable("question");
            modelBuilder.Entity<Choice>().ToTable("choice");

            // Relaties
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Category)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.Category_Id);

            modelBuilder.Entity<Choice>()
                .HasOne(c => c.Question)
                .WithMany(q => q.Choices)
                .HasForeignKey(c => c.Question_Id);
        }
    }
}
