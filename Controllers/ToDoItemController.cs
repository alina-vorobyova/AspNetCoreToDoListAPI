using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoListDbContext context;
        private readonly IMapper mapper;

        public ToDoItemController(ToDoListDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        /// <summary>
        /// Create new ToDoItem
        /// </summary>
        /// <param name="toDoItemDTO">ToDoItem object</param>
        /// <returns>ToDoItem</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ToDoItemDTO> Create(ToDoItemDTO toDoItemDTO)
        {
            if (toDoItemDTO != null)
            {
                var toDoList = context.ToDoLists.Find(toDoItemDTO.ToDoListId);
                if (toDoList != null)
                {
                    var toDoItem = mapper.Map<ToDoItem>(toDoItemDTO);
                    context.ToDoItems.Add(toDoItem);
                    context.SaveChanges();
                    toDoItemDTO = mapper.Map<ToDoItemDTO>(toDoItem);
                    return toDoItemDTO;
                }
                else
                   return BadRequest("ToDoList Not found!");
            }
            else
                return BadRequest();
        }


        /// <summary>
        /// Get all ToDoItems
        /// </summary>
        /// <returns>All ToDoItems</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ToDoItemDTO>> GetAll()
        {
            var toDoItems = context.ToDoItems.ToList();

            if (toDoItems.Count == 0)
                return NotFound();

            var dtos = mapper.Map<List<ToDoItemDTO>>(toDoItems);

            return dtos;
        }

        /// <summary>
        /// Get ToDoItem by Id
        /// </summary>
        /// <param name="id">ToDoItem Id</param>
        /// <returns>ToDoItem</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ToDoItemDTO> GetById(int id)
        {
            var toDoItem = context.ToDoItems.Find(id);

            if (toDoItem != null)
            {
                var toDoItemDto = mapper.Map<ToDoItemDTO>(toDoItem);
                return toDoItemDto;
            }
            else
                return NotFound();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var toDoItem = context.ToDoItems.Find(id);
            if (toDoItem != null)
            {
                context.ToDoItems.Remove(toDoItem);
                context.SaveChanges();
                return NoContent();
            }
            else
                return NotFound();
        }
       
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ToDoItemDTO> Replace(int id, ToDoItemDTO toDoItemDTO)
        {
            toDoItemDTO.Id = id;

            var toDoItem = mapper.Map<ToDoItem>(toDoItemDTO);

            try
            {
                context.ToDoItems.Update(toDoItem);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (context.ToDoItems.Find(id) == null)
                    return NotFound();
                else
                    throw;
            }

            toDoItemDTO = mapper.Map<ToDoItemDTO>(toDoItem);

            return toDoItemDTO;
        }
    }
}
