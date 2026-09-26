using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetByOrderIdAsync(int orderId);
        Task<OrderItemDto?> GetByIdAsync(int id);
    }
}
