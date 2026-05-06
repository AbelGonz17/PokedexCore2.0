using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokedexCore.Domain.Interfaces;
using PokedexCore.Infrastructure.Context;
using PokedexCore.Infrastructure.Repositories;
using System.Reflection;

namespace PokedexCore.Infrastructure
{
    public static class InfrastructureCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PokedexCoreDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(PokedexCoreDbContext).Assembly.FullName)
                ));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<PokedexCoreDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<PokedexCoreDbContext>());

            return services;
        }
    }
}