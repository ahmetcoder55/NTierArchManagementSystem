using DataAccess.Abstract.Repositories;
using DataAccess.Abstract.UnitOfWorks;
using DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        private readonly IOrderRepository _orderRepository;

        public UnitOfWork(AppDbContext context, ICategoryRepository categoryRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
        }

        public ICategoryRepository Category => _categoryRepository;

        public IProductRepository Product => _productRepository;

        public IOrderItemRepository OrderItem => _orderItemRepository;

        public IOrderRepository Order => _orderRepository;

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
