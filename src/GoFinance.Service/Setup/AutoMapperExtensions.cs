using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoFinance.Service.Setup
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMappers(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
