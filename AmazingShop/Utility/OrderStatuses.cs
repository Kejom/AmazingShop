using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Utility
{
    public enum OrderStatuses
    {
        [Display(Name = "Utworzony")]
        Created,
        [Display(Name = "Przetwarzany")]
        Processed,
        [Display(Name = "Wysłany")]
        Shipped,
        [Display(Name = "Anulowany")]
        Canceled,
    }
}
