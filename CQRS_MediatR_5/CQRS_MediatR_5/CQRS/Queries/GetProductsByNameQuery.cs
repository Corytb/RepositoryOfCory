using CQRS_MediatR_5.Models;
using MediatR;

namespace CQRS_MediatR_5.CQRS.Queries
{
    public class GetProductsByNameQuery : IRequest<IEnumerable<Product>>
    {
        public string Name { get; set; }

    }
}
