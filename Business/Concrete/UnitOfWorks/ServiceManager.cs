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

        public ServiceManager(IProductService productService)
        {
            _productService = productService;
        }

        public IProductService ProductService => _productService;
    }
}
