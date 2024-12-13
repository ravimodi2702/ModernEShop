using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Application.Dto;
using Test.Application.HttpClient;
using Test.Application.Repositories;
using Test.Core;

namespace Test.Application.UseCases.GetData
{
    public class GetAllDataHandler : IRequestHandler<GetAllDataQuery, IList<StudentDto>>
    {

        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetAllDataQuery> _validator;
        private readonly ILogger<GetAllDataHandler> _logger;
        private readonly ITestHttpClient _testHttpClient;
        public GetAllDataHandler(IRepository repository, IMapper mapper, IValidator<GetAllDataQuery> validator, ILogger<GetAllDataHandler> logger, ITestHttpClient httpClient)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
            _testHttpClient = httpClient;
        }
        public async Task<IList<StudentDto>> Handle(GetAllDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("inside getl all daa handler");
            await _validator.ValidateAndThrowAsync(request);
            await _testHttpClient.GetAllStudent();
            var data = await _repository.GetData(request.Id);
            return _mapper.Map<IList<StudentDto>>(data);
        }
    }
}
