using Business.Abstract.Services;
using Business.Abstract.UnitOfWorks;
using Business.Concrete.Mapping;
using Business.Concrete.Services;
using Business.Concrete.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Extensions
{
    public static class BusinessExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(x=>x.AddMaps(typeof(GeneralMapping)));

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<IServiceManager, ServiceManager>();
        }
     
    }
}
