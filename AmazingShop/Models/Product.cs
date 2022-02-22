using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nazwa Produktu jest wymagana")]
        [Display(Name = "Nazwa Produktu")]
        public string Name { get; set; }
        [Display(Name = "Ukryj Produkt")]
        public bool Hidden { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Cena musi być wyższa niż 0 zł")]
        [Display(Name = "Cena")]
        public double Price { get; set; }
        [Display(Name = "Skrócony Opis")]
        public string ShortDescription { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Zdjęcie Produktu")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Kategoria jest wymagana")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
