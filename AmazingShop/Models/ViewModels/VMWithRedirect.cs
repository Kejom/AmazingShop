using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Models.ViewModels
{
    public class VMWithRedirect<T> where T: class
    {
        public T Model { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

    }
}
