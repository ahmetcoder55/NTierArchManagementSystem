using Business.Abstract.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.UnitOfWorks
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
    }
}
