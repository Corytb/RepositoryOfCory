using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CQRS_MediatR_5.Models;

namespace CQRS_MediatR_5.Data
{
    public class CQRS_MediatR_5Context : DbContext
    {
        public CQRS_MediatR_5Context (DbContextOptions<CQRS_MediatR_5Context> options)
            : base(options)
        {
        }

        public DbSet<CQRS_MediatR_5.Models.Product> Product { get; set; } = default!;
    }
}
