using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa kategorii jest wymagana")]
        [DisplayName("Nazwa Kategorii")]
        public string Name { get; set; }
        [DisplayName("Kolejność Wyświetlania")]
        [Required(ErrorMessage = "Kolejność wyświetlania jest wymagana i musi być wyższa niż 0")]
        [Range(1, int.MaxValue, ErrorMessage = "Kolejność wyświetlania musi być wyższa niż 0")]
        public int DisplayOrder { get; set; }
    }
}
