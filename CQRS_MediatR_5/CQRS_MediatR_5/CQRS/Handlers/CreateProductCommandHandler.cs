using CQRS_MediatR_5.CQRS.Commands;
using CQRS_MediatR_5.Data;
using CQRS_MediatR_5.Models;
using MediatR;

namespace CQRS_MediatR_5.CQRS.Handlers
{

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private CQRS_MediatR_5Context context;
        public CreateProductCommandHandler(CQRS_MediatR_5Context context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product();
            product.Name = command.Name;
            product.Price = command.Price;

            context.Product.Add(product);
            await context.SaveChangesAsync();
            return product.Id;
        }
    }
}
