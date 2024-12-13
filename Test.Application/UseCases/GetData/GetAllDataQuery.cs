using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Application.Dto;
using Test.Core;

namespace Test.Application.UseCases.GetData
{
    public class GetAllDataQuery : IRequest<IList<StudentDto>>
    {
        public int Id { get; set; }
        public GetAllDataQuery(int id)
        {
            this.Id = id;
        }
    }
}
