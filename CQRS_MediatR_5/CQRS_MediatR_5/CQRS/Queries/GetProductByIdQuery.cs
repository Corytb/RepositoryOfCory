using CQRS_MediatR_5.Data;
using CQRS_MediatR_5.Models;
using MediatR;
using System.Data.Entity;

namespace CQRS_MediatR_5.CQRS.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {

        public int Id { get; set; }

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
}
