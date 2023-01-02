using CQRS_MediatR_5.CQRS.Commands;
using CQRS_MediatR_5.Data;
using MediatR;

namespace CQRS_MediatR_5.CQRS.Handlers
{

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private CQRS_MediatR_5Context context;
        public UpdateProductCommandHandler(CQRS_MediatR_5Context context)
        {
            this.context = context;
        }
        public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {

            //using "Async" causing issues in some commands/queries.
            //"InvalidOperationException: The provider for the source IQueryable doesn't implement
            //IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used
            //for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068."
            //var product = await context.Product.Where(a => a.Id == command.Id).FirstOrDefaultAsync();

            var product = context.Product.Where(a => a.Id == command.Id).FirstOrDefault();

            if (product == null)
            {
                return default;
            }
            else
            {
                product.Name = command.Name;
                product.Price = command.Price;
                await context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
