using Business.Abstract.Services;
using Business.Abstract.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.UnitOfWorks
{
    public class ServiceManager : IServiceManager
    {
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        private readonly IOrderService _orderService;

        private readonly IOrderItemService _orderItemService;

        public ServiceManager(IProductService productService, ICategoryService categoryService, IOrderService orderService, IOrderItemService orderItemService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        public IProductService ProductService => _productService;

        public ICategoryService CategoryService => _categoryService;

        public IOrderService OrderService => _orderService;

        public IOrderItemService OrderItemService => _orderItemService;
    }
}
