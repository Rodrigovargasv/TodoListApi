using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappings
{
    public class MappingsEntityDTOs : Profile
    {
        public MappingsEntityDTOs() 
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
        
    }
}
