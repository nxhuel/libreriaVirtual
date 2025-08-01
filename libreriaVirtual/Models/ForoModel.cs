using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreriaVirtual.Models
{
    public class ForoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

        public List<MessageModel>? Messages { get; } = new List<MessageModel>();
    }
}
