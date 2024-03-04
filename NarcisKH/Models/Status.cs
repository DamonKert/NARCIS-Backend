using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NarcisKH.Models
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderStatusId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? DeliveryStatusId { get; set; }
        public int? PaymentMethodId { get; set; }
        [ForeignKey("OrderStatusId")]
        [Required]
        public virtual OrderStatus OrderStatus { get; set; }
        [ForeignKey("PaymentStatusId")]
        public virtual PaymentStatus? PaymentStatus { get; set; }
        [ForeignKey("DeliveryStatusId")]
        public virtual DeliveryStatus? DeliveryStatus { get; set; }
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod? PaymentMethod { get; set; }

    }
}
