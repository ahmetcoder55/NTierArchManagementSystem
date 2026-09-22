using DataAccess.Abstract.Repositories;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.Repositories
{
    public class CategoryRepository : EfRepositoryBase<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
