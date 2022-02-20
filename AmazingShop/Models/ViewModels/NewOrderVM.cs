﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models.ViewModels
{
    public class NewOrderVM
    {
        public AppUser User { get; set; }
        [Required(ErrorMessage = "Adres jest wymagany")]
        public int SelectedAddressId { get; set; }
        public IEnumerable<SelectListItem> Addresses { get; set; }
        public IEnumerable<CartProductVM> Products { get; set; }
    }
}
