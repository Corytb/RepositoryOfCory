using CQRS_MediatR_5.CQRS.Commands;
using CQRS_MediatR_5.CQRS.Queries;
using CQRS_MediatR_5.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CQRS_MediatR_5.Controllers
{


    public class ProductsMediatRController : Controller
    {
        private IMediator _mediator;
        public ProductsMediatRController(IMediator mediator)
        {

            this._mediator = mediator;
        }

        //private readonly CQRS_MediatR_5Context _context;

        //public ProductsMediatRController(CQRS_MediatR_5Context context)
        //{
        //    _context = context;
        //}

        public async Task<IActionResult> Index(GetAllProductsQuery query)
        {
            var result = await _mediator.Send(query);
            return View(result);
        }

        public async Task<IActionResult> Search(GetProductsByNameQuery query)
        {
            var result = await _mediator.Send(query);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(GetProductByIdQuery query)
        {
            var result = await _mediator.Send(query);
            //var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
            return View(result);
        }

        public async Task<IActionResult> Edit(GetProductByIdQuery query)
        {
            var result = await _mediator.Send(query);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(GetProductByIdQuery query)
        {
            var result = await _mediator.Send(query);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteProductByIdCommand command)
        {
            var result = await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
