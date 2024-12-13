using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Application.Dto;
using Test.Application.UseCases.GetData;

namespace Test.Api.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<TestController> _logger;

        public TestController(IMediator mediator, ILogger<TestController> logger) {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IList<StudentDto>>> Get(int id)
        {
            var a = await _mediator.Send(new GetAllDataQuery(id));
            return Ok(a);
        }
    }
}
