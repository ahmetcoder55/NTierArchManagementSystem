using Entities.Abstract;
using Entities.Abstract.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Order:BaseEntity,IEntity
    {
        public string CustomerId { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
