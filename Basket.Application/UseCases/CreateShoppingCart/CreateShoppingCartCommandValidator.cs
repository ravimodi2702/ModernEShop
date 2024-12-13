using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.UseCases.CreateShoppingCart
{
    public class CreateShoppingCartCommandValidator : AbstractValidator<CreateShoppingCartCommand>
    {
        public CreateShoppingCartCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.UserName).Length(0, 4);
        }
    }
}
