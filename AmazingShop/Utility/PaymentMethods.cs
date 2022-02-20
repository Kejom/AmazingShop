using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Utility
{
    public enum PaymentMethods
    {
        [Display(Name ="Za Pobraniem")]
       CashOnDelivery,
        //[Display(Name = "Przelew Bankowy")]
        //BankTransfer,
        //[Display(Name = "Płatność Kartą")]
        //CreditCard

    }
}
