using CQRS_MediatR.CQRS.Commands;
using CQRS_MediatR.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CQRS_MediatR.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllProductQuery query)
        {
            return Ok(await _mediator.Send(query));
            //var result = await _mediator.Send(query);
            //return View("GetAllProductsView", result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _mediator.Send(new GetProductByNameQuery { Name = name }));
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    return Ok(await _mediator.Send(new GetProductByIdQuery { Id = id }));
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _mediator.Publish(new Notifications.DeleteProductNotification { ProductId = id });
            return Ok(await _mediator.Send(new DeleteProductByIdCommand { Id = id }));
        }
    }
}
