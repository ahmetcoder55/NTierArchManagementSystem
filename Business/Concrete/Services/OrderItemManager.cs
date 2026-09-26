using AutoMapper;
using Business.Abstract.Services;
using DataAccess.Abstract.UnitOfWorks;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Services
{
    public class OrderItemManager : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDto>> GetByOrderIdAsync(int orderId)
        {
            var items = _unitOfWork.OrderItem.GetWhere(x => x.OrderId == orderId, trackChanges: false);
            return _mapper.Map<IEnumerable<OrderItemDto>>(items);
        }

        public async Task<OrderItemDto?> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.OrderItem.GetByIdAsync(id, trackChanges: false);
            return _mapper.Map<OrderItemDto>(item);
        }
    }
}
