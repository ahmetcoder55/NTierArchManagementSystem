using DataAccess.Abstract.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract.UnitOfWorks
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public ICategoryRepository Category { get;  }
        public IProductRepository Product { get;  }
        public IOrderItemRepository OrderItem { get; }
        public IOrderRepository Order { get; }

        Task<int> SaveChangesAsync();
    }
}
