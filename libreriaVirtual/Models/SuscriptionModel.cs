using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreriaVirtual.Models
{
    public class SuscriptionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

        public DateTime? PaymentDate { get; set; }

        public bool? IsActive { get; set; }

        public string? PaymentMethod { get; set; }
    }
}
