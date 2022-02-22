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
        public int OrderStatus { get; set; }
        [Display(Name = "Metoda Płatności")]
        public int PayymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        [Required]
        [Display(Name = "Telefon kontaktowy")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Odbiorca")]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Pole Kraj nie może być puste")]
        [Display(Name = "Kraj")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Pole Miasto nie może być puste")]
        [Display(Name = "Miasto")]
        public string City { get; set; }
        [Required(ErrorMessage = "Pole Ulica nie może być puste")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Pole Numer Budynku nie może być puste")]
        [Range(1, int.MaxValue, ErrorMessage = "Numer Budynku musi być liczbą dodatnią")]
        [Display(Name = "Numer Budynku")]
        public int BuildingNumber { get; set; }
        [Display(Name = "Numer Mieszkania")]
        public int LocalNumber { get; set; }
        [Required(ErrorMessage = "Pole Kod Pocztowy nie może być puste")]
        [RegularExpression(@"^(?=.*\d)[_0-9]{2}-[_0-9]{3}$", ErrorMessage = "Niepoprawny format kodu pocztowego")]
        [Display(Name = "Kod Pocztowy")]
        public string ZipPostalCode { get; set; }
    }
}
