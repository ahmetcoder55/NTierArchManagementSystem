using Entities.Abstract;
using Entities.Abstract.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Category:BaseEntity,IEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
