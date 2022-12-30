using CQRS_MediatR_5.Data;
using CQRS_MediatR_5.Models;
using MediatR;
using System.Data.Entity;
//using Microsoft.EntityFrameworkCore;

namespace CQRS_MediatR_5.CQRS.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {

    }
}
