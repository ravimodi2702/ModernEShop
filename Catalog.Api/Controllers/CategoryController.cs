using Catalog.Application.ResponseDto;
using Catalog.Application.UseCases.AddCategory;
using Catalog.Application.UseCases.GetAllCategories;
using Catalog.Application.UseCases.GetCategoriesById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;

        public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IList<CategoryDto>), 200)]
        [ProducesResponseType(typeof(IList<CategoryDto>), 204)]
        public async Task<ActionResult<IList<Core.Category>>> Get()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            if (categories != null && categories.Count > 0)
                return Ok(categories);
            else
                return NoContent();
        }


        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var query = new GetCategoriesByIdQuery(id);
            var categories = await _mediator.Send(query);
            return Ok(categories);

        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(IList<CategoryDto>), 200)]
        public async Task<IList<CategoryDto>> Post(AddCategoryCommand addCategoryCommand)
        {
            return await _mediator.Send(addCategoryCommand);
        }
    }
}
