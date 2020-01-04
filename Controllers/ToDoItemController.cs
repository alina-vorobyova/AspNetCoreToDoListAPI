using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.Data;

namespace ToDoListAPI.Controllers
{
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoListDbContext context;
        private readonly IMapper mapper;

        public ToDoItemController(ToDoListDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

       
    }
}
