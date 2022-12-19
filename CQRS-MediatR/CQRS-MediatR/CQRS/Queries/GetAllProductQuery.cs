using CQRS_MediatR.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS_MediatR.CQRS.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Product>>
        {
            private ProductContext context;
            public GetAllProductQueryHandler(ProductContext context)
            {
                this.context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
            {
                var productList = await context.Product.ToListAsync();
                return productList;
            }
        }
    }
}
