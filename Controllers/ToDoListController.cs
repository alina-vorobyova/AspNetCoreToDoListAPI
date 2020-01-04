using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.Data;
using ToDoListAPI.Model;
using ToDoListAPI.Model.DTO;

namespace ToDoListAPI.Controllers
{

    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoListDbContext context;
        private readonly IMapper mapper;

        public ToDoListController(ToDoListDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        /// <summary>
        ///     Get all ToDoLists
        /// </summary>
        /// <returns>ToDoLists</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ToDoListDTO>> GetAll()
        {
            var toDoLists = context.ToDoLists.ToList();

            if (toDoLists.Count == 0)
                return NotFound();

            var dtos = mapper.Map<List<ToDoListDTO>>(toDoLists);

            return dtos;
        }


        /// <summary>
        ///     Add a new ToDoList
        /// </summary>
        /// <param name="toDoListDTO">ToDoList object</param>
        /// <returns>Created toDoList</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ToDoListDTO> Create(ToDoListDTO toDoListDTO)
        {
            if (toDoListDTO != null)
            {
                var toDoList = mapper.Map<ToDoList>(toDoListDTO);
                context.ToDoLists.Add(toDoList);
                context.SaveChanges();
                toDoListDTO = mapper.Map<ToDoListDTO>(toDoList);
                return toDoListDTO;
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var toDoList = context.ToDoLists.Find(id);
            if (toDoList == null)
                return NotFound();

            context.ToDoLists.Remove(toDoList);
            context.SaveChanges();

            return NoContent();
        }
    }
}
