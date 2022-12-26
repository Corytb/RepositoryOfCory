using CQRS_MediatR.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatR_Vievvs.Controllers
{
    public class ProductController : Controller
    {
        private IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllProductQuery query)
        {
            var result = await _mediator.Send(query);
            return View(result);
        }
    }
}
