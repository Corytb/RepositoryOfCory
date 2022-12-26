using CQRS_MediatR_5.Data;
using CQRS_MediatR_5.Models;
using MediatR;
using System.Data.Entity;
//using Microsoft.EntityFrameworkCore;

namespace CQRS_MediatR_5.CQRS.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
        {
            private CQRS_MediatR_5Context context;
            public GetAllProductsQueryHandler(CQRS_MediatR_5Context context)
            {
                this.context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                var productList = context.Product.ToList();
                return productList;
            }
        }
    }
}
