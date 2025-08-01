using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreriaVirtual.Models
{
    public class BookFavModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel? User { get; set; }
        public int? BookId { get; set; }
        [ForeignKey("BookId")]
        public BookModel? Book { get; set; }

        public DateTime? DateAdded { get; set; }

    }
}
