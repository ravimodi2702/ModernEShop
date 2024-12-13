using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Catalog.Application.UseCases.AddCategory
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryValidator()
        {

            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotNull();
        }
    }
}
