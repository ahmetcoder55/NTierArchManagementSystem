using Business.Concrete.Mapping;
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
        }
     
    }
}
