using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Test.Application.Dto;
using Test.Core;

namespace Test.Application.Mappers
{
    public class TestMapperProfile : Profile
    {
        public TestMapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
