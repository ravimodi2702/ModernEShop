using Catalog.Application.ResponseDto;
using Catalog.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.UseCases.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IList<CategoryDto>>
    {
    }
}
