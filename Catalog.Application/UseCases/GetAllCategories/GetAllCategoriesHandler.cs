using AutoMapper;
using Catalog.Application.Repositories;
using Catalog.Application.ResponseDto;
using Catalog.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IList<CategoryDto>>
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IList<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories =  await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<IList<CategoryDto>>(categories);
        }
    }
}
