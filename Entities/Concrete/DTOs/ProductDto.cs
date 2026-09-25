using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.DTOs
{
    public record ProductDto(int Id, string Name, string Description, decimal Price, int Stock, int CategoryId, string CategoryName);
}
