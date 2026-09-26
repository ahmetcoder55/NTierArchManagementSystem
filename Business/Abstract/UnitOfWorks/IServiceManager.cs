using Business.Abstract.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.UnitOfWorks
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public ICategoryService CategoryService { get; }
        public IOrderService OrderService    { get; }

        public IOrderItemService OrderItemService { get; }
    }
}
