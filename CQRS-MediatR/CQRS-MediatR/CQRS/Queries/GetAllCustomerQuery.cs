using CQRS_MediatR.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS_MediatR.CQRS.Queries
{
    public class GetAllCustomerQuery : IRequest<IEnumerable<Customer>>
    {
        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<Customer>>
        {
            private ProductContext context;
            public GetAllCustomerQueryHandler(ProductContext context)
            {
                this.context = context;
            }
            public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
            {
                var customerList = await context.Customer.ToListAsync();
                return customerList;
            }
        }
    }
}
