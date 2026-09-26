using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task<OrderDto> CreateAsync(OrderCreateDto dto);
        Task DeleteAsync(int id);
    }
}
