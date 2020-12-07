using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Models
{
    public class CreateTransactionVewModel
    {
        // Drop down for products
        public IEnumerable<SelectListItem> Products { get; set; }
        // Selected Product
        public int ProductId { get; set; }
        // Drop down customers
        public IEnumerable<SelectListItem> Customers { get; set; }
        // Selected customers
        public int CustomerId { get; set; }
    }
}