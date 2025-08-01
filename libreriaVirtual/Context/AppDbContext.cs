using libreriaVirtual.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace libreriaVirtual.Context
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> User { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<BookModel> Book { get; set; }
        public DbSet<BookFavModel> BookFav { get; set; }
        public DbSet<ReviewModel> Review { get; set; }
        public DbSet<SuscriptionModel> Suscription { get; set; }
        public DbSet<ForoModel> Foro { get; set; }
        public DbSet<MessageModel> Message { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // relation User - BooksCreated (Books)
            modelBuilder.Entity<BookModel>()
                .HasOne(book => book.User)
                .WithMany(user => user.BooksCreated)
                .HasForeignKey(book => book.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation User - BooksFav
            modelBuilder.Entity<BookFavModel>()
                .HasOne(bookFav => bookFav.User)
                .WithMany(user => user.BooksFav)
                .HasForeignKey(bookFav => bookFav.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation Book - BooksFav
            modelBuilder.Entity<BookFavModel>()
                .HasOne(bookFav => bookFav.Book)
                .WithMany(book => book.BooksFav)
                .HasForeignKey(bookFav => bookFav.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation Category - Books
            modelBuilder.Entity<BookModel>()
                .HasOne(book => book.Category)
                .WithMany(category => category.Books)
                .HasForeignKey(book => book.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // relation User - Reviews 
            modelBuilder.Entity<ReviewModel>()
                .HasOne(review => review.User)
                .WithMany(user => user.Reviews)
                .HasForeignKey(review => review.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation Book - Reviews
            modelBuilder.Entity<ReviewModel>()
                .HasOne(review => review.Book)
                .WithMany(book => book.Reviews)
                .HasForeignKey(review => review.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation User - Foros
            modelBuilder.Entity<ForoModel>()
                .HasOne(foro => foro.User)
                .WithMany(user => user.Foros)
                .HasForeignKey(foro => foro.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation User - Messages
            modelBuilder.Entity<MessageModel>()
                .HasOne(message => message.User)
                .WithMany(user => user.Messages)
                .HasForeignKey(message => message.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation Foro - Messages
            modelBuilder.Entity<MessageModel>()
                .HasOne(message => message.Foro)
                .WithMany(foro => foro.Messages)
                .HasForeignKey(message => message.ForoId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation User - Sucriptions
            modelBuilder.Entity<SuscriptionModel>()
                .HasOne(suscription => suscription.User)
                .WithOne(user => user.Suscription)
                .HasForeignKey<SuscriptionModel>(suscription => suscription.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}
