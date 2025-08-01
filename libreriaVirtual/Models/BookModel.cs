using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreriaVirtual.Models
{
    public class BookModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? UrlPdf { get; set; }

        public string? FrontPage { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryModel? Category { get; set; }

        public string? Type { get; set; }

        public string? Author { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

        public List<BookFavModel>? BooksFav { get; } = new List<BookFavModel>();


        public List<ReviewModel>? Reviews { get; } = new List<ReviewModel>();
    }
}
