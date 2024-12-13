using Catalog.Application.ResponseDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.UseCases.AddCategory
{
    public class AddCategoryCommand : IRequest<IList<CategoryDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
