using System;
using System.Collections.Generic;

namespace EShopAPI.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int ProductsId { get; set; }
        public string ProductName { get; set; }
        public int? Size { get; set; }
        public string Varienty { get; set; }
        public double? Price { get; set; }
        public string Status { get; set; }

        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
