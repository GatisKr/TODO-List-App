using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.StorageService;

namespace TodoApp.Controllers
{
    public class TodoItemsController : Controller
    {
        private readonly IEntityService<TodoItem> _todoItemService;

        public TodoItemsController(IEntityService<TodoItem> todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // GET: TodoItems
        public async Task<IActionResult> Index()
        {
            return View(await _todoItemService.GetAllAsync());
        }

        // GET: TodoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _todoItemService.GetByIdAsync(id.Value);

            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                await _todoItemService.AddAsync(todoItem);
                return RedirectToAction(nameof(Index));
            }

            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _todoItemService.GetByIdAsync(id.Value);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _todoItemService.UpdateAsync(todoItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TodoItemExists(todoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _todoItemService.GetByIdAsync(id.Value);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _todoItemService.GetByIdAsync(id);
            if (todoItem != null)
            {
                await _todoItemService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TodoItemExists(int id)
        {
            return await _todoItemService.ExistsAsync(id);
        }
    }
}
