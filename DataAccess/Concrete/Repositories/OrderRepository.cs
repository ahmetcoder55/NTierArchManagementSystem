using DataAccess.Abstract.Repositories;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.Repositories
{
    public class OrderRepository : EfRepositoryBase<Order, AppDbContext>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
