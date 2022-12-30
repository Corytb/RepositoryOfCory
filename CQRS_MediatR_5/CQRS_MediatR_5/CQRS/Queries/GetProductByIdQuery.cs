using CQRS_MediatR_5.Data;
using CQRS_MediatR_5.Models;
using MediatR;
using System.Data.Entity;

namespace CQRS_MediatR_5.CQRS.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {

        public int Id { get; set; }

    }
}
