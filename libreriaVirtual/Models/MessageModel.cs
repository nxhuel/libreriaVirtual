using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreriaVirtual.Models
{
    public class MessageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Message { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

        public int? ForoId { get; set; }
        [ForeignKey("ForoId")]
        public ForoModel? Foro { get; set; }


    }
}
