using CQRS_MediatR.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CQRS_MediatR.CQRS.Queries
{
    public class GetProductByNameQuery : IRequest<IEnumerable<Product>>
    {
        public string Name { get; set; }

        public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IEnumerable<Product>>
        {

            private ProductContext context;
            public GetProductByNameQueryHandler(ProductContext context)
            {
                this.context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetProductByNameQuery query, CancellationToken cancellationToken)
            {
                //var product = await context.Product.Where(a => a.Name == query.Name).FirstOrDefaultAsync();


                var productList = context.Product.AsNoTracking()
                     .Where(a => a.Name.Contains(query.Name))
                     .ToList();
                return productList;
            }
        }
    }
}
