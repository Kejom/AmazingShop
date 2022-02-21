using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Utility
{
    public enum OrderStatuses
    {
        [Display(Name = "Utworzone")]
        Created,
        [Display(Name = "Przetwarzane")]
        Processed,
        [Display(Name = "Wysłane")]
        Shipped,
        [Display(Name = "Anulowane")]
        Canceled,
    }
}
