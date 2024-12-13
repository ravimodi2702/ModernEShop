using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Application.UseCases.GetData
{
    public class GetAllDataValidator : AbstractValidator<GetAllDataQuery>
    {
        public GetAllDataValidator()
        {
            RuleFor(x => x.Id).NotNull();

        }
    }
}
