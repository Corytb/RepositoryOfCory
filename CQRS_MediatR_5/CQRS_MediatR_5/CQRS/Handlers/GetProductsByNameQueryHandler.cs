using CQRS_MediatR_5.CQRS.Queries;
using CQRS_MediatR_5.Data;
using CQRS_MediatR_5.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CQRS_MediatR_5.CQRS.Handlers
{

    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, IEnumerable<Product>>
    {
        private CQRS_MediatR_5Context context;
        public GetProductsByNameQueryHandler(CQRS_MediatR_5Context context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Product>> Handle(GetProductsByNameQuery query, CancellationToken cancellationToken)
        {

            var productList = context.Product.Where(a => a.Name == query.Name).ToList();
            //var productList = context.Product.AsNoTracking()
            //     .Where(a => a.Name.Contains(request.Name))
            //     .ToList();

            //var productList = context.Product.Where(a => a.Name == request.Name).ToList();
            return productList;
        }
    }
}
