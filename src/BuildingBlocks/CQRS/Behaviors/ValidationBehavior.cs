using BuildingBlocks.CQRS.Commands;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : class, ICommand<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context =
                    new FluentValidation.ValidationContext<TRequest>(request);

                var validationResults =
                    await Task.WhenAll
                    (_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures =
                    validationResults
                    .SelectMany(current => current.Errors)
                    .Where(current => current != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    throw new FluentValidation.ValidationException(errors: failures);
                }
            }

            return await next();
        }
    }
}
