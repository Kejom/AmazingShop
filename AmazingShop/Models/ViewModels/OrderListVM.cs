using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models.ViewModels
{
    public class OrderListVM
    {
        public IEnumerable<OrderHeader> Orders { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        public int Status { get; set; }
        public bool IsAdmin { get; set; }
    }
}
