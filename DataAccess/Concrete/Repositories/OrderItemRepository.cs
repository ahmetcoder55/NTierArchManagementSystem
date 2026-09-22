using DataAccess.Abstract.Repositories;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Repositories
{
    public class OrderItemRepository : EfRepositoryBase<OrderItem, AppDbContext>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
