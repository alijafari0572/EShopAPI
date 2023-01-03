using System;
using System.Collections.Generic;

namespace EShopAPI.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrdetId { get; set; }
        public DateTime? Date { get; set; }
        public double? Totak { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public int SalesPersonId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual SalesPersons SalesPerson { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
