using CQRS_MediatR_5.CQRS.Commands;
using CQRS_MediatR_5.Data;
using MediatR;

namespace CQRS_MediatR_5.CQRS.Handlers
{

    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, int>
    {
        private CQRS_MediatR_5Context context;
        public DeleteProductByIdCommandHandler(CQRS_MediatR_5Context context)
        {
            this.context = context;
        }
        public async Task<int> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var product = context.Product.Where(a => a.Id == command.Id).FirstOrDefault();
            context.Product.Remove(product);
            await context.SaveChangesAsync();
            return product.Id;
        }
    }
}
