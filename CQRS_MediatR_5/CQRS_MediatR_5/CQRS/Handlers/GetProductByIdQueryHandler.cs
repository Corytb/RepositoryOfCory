using CQRS_MediatR_5.CQRS.Queries;
using CQRS_MediatR_5.Data;
using CQRS_MediatR_5.Models;
using MediatR;

namespace CQRS_MediatR_5.CQRS.Handlers
{

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private CQRS_MediatR_5Context context;
        public GetProductByIdQueryHandler(CQRS_MediatR_5Context context)
        {
            this.context = context;
        }

        public async Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {

            var product = context.Product.Where(a => a.Id == query.Id).FirstOrDefault();
            return product;
        }
    }
}
