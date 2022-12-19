using CQRS_MediatR.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS_MediatR.CQRS.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private ProductContext context; 
            public GetProductByIdQueryHandler(ProductContext context)
            {
                this.context = context;
            }

            public async Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {

                var product = await context.Product.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                return product;
            }
        }
    }
}
