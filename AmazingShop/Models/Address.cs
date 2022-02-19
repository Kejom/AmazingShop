
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
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
        [Display(Name = "Numer Budynku")]
        public int BuildingNumber { get; set; }
        [Display(Name = "Numer Mieszkania")]
        public int LocalNumber { get; set; }
        [RegularExpression(@"^(?=.*\d)[_0-9]{2}-[_0-9]{3}$", ErrorMessage = "Niepoprawny format kodu pocztowego")]
        [Display(Name = "Kod Pocztowy")]
        public string ZipPostalCode { get; set; }
        [Display(Name = "Id użytkownika")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
