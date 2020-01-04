using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.Model;
using ToDoListAPI.Model.DTO;

namespace ToDoListAPI.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ToDoList, ToDoListDTO>();
            CreateMap<ToDoListDTO, ToDoList>();

        }
    }
}
