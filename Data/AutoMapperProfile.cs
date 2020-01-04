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
            CreateMap<ToDoList, ToDoListDTO>()
                .ForMember(x => x.ToDoItems, x => x.MapFrom(y => y.ToDoItem));

            CreateMap<ToDoListDTO, ToDoList>();

            CreateMap<ToDoItem, ToDoItemDTO>();
            CreateMap<ToDoItemDTO, ToDoItem>();

        }
    }
}
