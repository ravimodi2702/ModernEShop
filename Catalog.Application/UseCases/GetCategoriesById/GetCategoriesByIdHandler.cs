using AutoMapper;
using Catalog.Application.Repositories;
using Catalog.Application.ResponseDto;
using Catalog.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.GetCategoriesById
{
    public class GetCategoriesByIdHandler : IRequestHandler<GetCategoriesByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoriesByIdHandler> _logger;
        private IMapper _mapper;
        public GetCategoriesByIdHandler(ICategoryRepository categoryRepository, ILogger<GetCategoriesByIdHandler> logger, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Handle(GetCategoriesByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetCategoriesByIdHandler: {request.CategoryId}");
            var categories = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            return _mapper.Map<CategoryDto>(categories);
        }
    }
}
