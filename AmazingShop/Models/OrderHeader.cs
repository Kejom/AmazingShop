using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        public string CreatedByUserId { get; set; }
        [ForeignKey("CreatedByUserId")]
        public AppUser CreatedBy { get; set; }

        [Required]
        [Display(Name = "Data Zamówienia")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Termin Wysyłki")]
        public DateTime ShippingDate { get; set; }
        [Required]
        [Display(Name = "Suma do zapłaty")]
        public double FinalOrderTotal { get; set; }
        [Display(Name = "Status Zamówienia")]
        public string OrderStatus { get; set; }
        [Display(Name = "Metoda Płatności")]
        public string PayymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        [Required]
        [Display(Name = "Telefon kontaktowy")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Odbiorca")]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Adres jest wymagany")]
        [Display(Name = "Adres")]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
    }
}
