using Microsoft.AspNetCore.Identity;

namespace libreriaVirtual.Models
{
    public class UserModel : IdentityUser
    {
        public List<BookModel>? BooksCreated { get; } = new List<BookModel>();

        public List<BookFavModel>? BooksFav { get; } = new List<BookFavModel>();

        public List<ReviewModel>? Reviews { get; } = new List<ReviewModel>();

        public List<ForoModel>? Foros { get; } = new List<ForoModel>();

        public List<MessageModel> Messages { get; } = new List<MessageModel>();

        public SuscriptionModel? Suscription { get; set; } 
    }
}
