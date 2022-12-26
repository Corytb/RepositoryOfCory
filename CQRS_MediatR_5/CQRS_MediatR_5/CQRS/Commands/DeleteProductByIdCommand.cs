using CQRS_MediatR_5.Data;
using CQRS_MediatR_5.Models;
using MediatR;
using System.Data.Entity;

namespace CQRS_MediatR_5.CQRS.Commands
{
    public class DeleteProductByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

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
}
