using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xurpas.Repository;
using Xurpas.Services;

namespace Xurpas
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IParkingRepository, ParkingRepository>();
            services.AddTransient<IParkingServices, ParkingServices>();

            //services.AddDbContext<ParkingDbContext>(opt => opt
            //    .UseSqlServer("Server=LAPTOP-BVN9LJ2I\\SQLEXPRESS;Database=XurpasTestDB;Trusted_Connection=True;MultipleActiveResultSets=true"));
            return services;
        }
    }
}
