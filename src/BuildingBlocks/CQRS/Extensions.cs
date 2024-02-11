using BuildingBlocks.CQRS.Behaviors;
using BuildingBlocks.CQRS.Commands;
using BuildingBlocks.CQRS.Commands.Dispatcher;
using BuildingBlocks.CQRS.Queris;
using BuildingBlocks.CQRS.Queris.Dispatcher;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows.Input;

namespace BuildingBlocks.CQRS
{
    public static class Extensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblies(typeof(ICommand<>).Assembly);
                c.AddOpenBehavior(typeof(ValidationBehavior<,>));
                c.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            services.AddValidatorsFromAssembly(typeof(ICommand<>).Assembly);
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            return services;
        }
    }
}
