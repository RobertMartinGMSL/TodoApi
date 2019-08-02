namespace TodoApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        /// <summary>
        /// This was created using MS docs tutorials, https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.1
        /// Please see doc for using Postman to call the WebAPI
        /// </summary>
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.TodoItems.Add(new TodoItem { Name = "Item2" });
                _context.SaveChanges();
            }
        }

        /* GET || /api/todo */
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            // default action
            // returns the list of ToDoItems
            return _context.TodoItems.ToList();
        }

        /* GET || /api/todo/3 */
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            // returns specific ToDoItem
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        /* POST || /api/todo/ */
        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            // creates ToDoItem
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            // returns ToDoItem that was created
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }


        /* PUT || /api/todo/3 */
        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            //finds existing ToDoItem by id
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            // updates that item with your PUT request body variables
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            // returns nothing as no return specified (like a void)
            return NoContent();
        }

        /* DELETE || /api/todo/3 */
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            //finds existing ToDoItem by id
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            // removes the item 
            _context.TodoItems.Remove(todo);
            _context.SaveChanges();

            // returns nothing as no return specified (like a void)
            return NoContent();
        }
    }
}