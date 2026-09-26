using AutoMapper;
using Business.Abstract.Services;
using DataAccess.Abstract.UnitOfWorks;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Services
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto dto)
        {
           if(dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var order = _mapper.Map<Order>(dto);
            if(order is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            await _unitOfWork.Order.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task DeleteAsync(int id)
        {
            var order =await _unitOfWork.Order.GetByIdAsync(id);
            _unitOfWork.Order.Delete(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<OrderDto>>(await _unitOfWork.Order.GetAllAsync());
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            return _mapper.Map<OrderDto>(await _unitOfWork.Order.GetByIdAsync(id));
        }
    }
}
