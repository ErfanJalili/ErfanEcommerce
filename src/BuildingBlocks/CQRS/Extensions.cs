using BuildingBlocks.CQRS.Behaviors;
using BuildingBlocks.CQRS.Commands;
using BuildingBlocks.CQRS.Commands.Dispatcher;
using BuildingBlocks.CQRS.Queris;
using BuildingBlocks.CQRS.Queris.Dispatcher;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows.Input;

namespace BuildingBlocks.CQRS
{
    public static class Extensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            services.AddValidatorsFromAssembly(assembly);

            //services.AddMediatR(c =>
            //{
            //    c.RegisterServicesFromAssemblies(typeof(ICommand<>).Assembly);
            //    c.AddOpenBehavior(typeof(ValidationBehavior<,>));
            //    c.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //});
            //services.AddValidatorsFromAssembly(typeof(ICommand<>).Assembly);
            //services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            //services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            return services;
        }
    }
}
