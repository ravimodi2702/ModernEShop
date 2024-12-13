using AutoMapper;
using Catalog.Application.HttpClients;
using Catalog.Application.Repositories;
using Catalog.Application.ResponseDto;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, IList<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IMockApiClient _mockApiClient;
        private readonly IValidator<AddCategoryCommand> _validator;
        public AddCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IMockApiClient mockApiClient, IValidator<AddCategoryCommand> validator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _mockApiClient = mockApiClient;
            _validator = validator;
        }

        public async Task<IList<CategoryDto>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            //var a = await _mockApiClient.GetData("2");
            //var b = await _mockApiClient.PostData(new MockReq { Name = "morpheus", Job = "leader" });
            await _categoryRepository.AddCategoryAsync(new Core.Category { Id = request.Id, Name = request.Name });
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<IList<CategoryDto>>(categories);
        }
    }
}
