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

        public ServiceManager(IProductService productService, ICategoryService categoryService, IOrderService orderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
        }

        public IProductService ProductService => _productService;

        public ICategoryService CategoryService => _categoryService;

        public IOrderService OrderService => _orderService;
    }
}
