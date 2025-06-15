using Microsoft.AspNetCore.Mvc;
using Task3.Models;
using Task3.Services;

namespace Task3.Controllers
{
    public class TodoController : AuthenticatedController
    {
        private readonly MongoService _todoService;

        public TodoController(MongoService todoService)
        {
            _todoService = todoService;
        }

        private string GetCurrentUsername()
        {
            return HttpContext.Session.GetString("username");
        }

        public IActionResult Index(string filter = "all")
        {
            var username = GetCurrentUsername();
            if (username == null)
                return RedirectToAction("Login", "Account");

            var todos = _todoService.GetByUser(username);

            todos = filter switch
            {
                "done" => todos.Where(t => t.IsDone).ToList(),
                "undone" => todos.Where(t => !t.IsDone).ToList(),
                _ => todos
            };

            ViewBag.Filter = filter;
            ViewBag.UserName = HttpContext.Session.GetString("name");
            return View(todos);
        }

        public IActionResult Create()
        {
            if (GetCurrentUsername() == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult Create(string title)
        {
            var username = GetCurrentUsername();
            if (string.IsNullOrWhiteSpace(title) || username == null)
                return RedirectToAction("Index");

            var newTask = new TodoItem
            {
                Title = title,
                IsDone = false,
                Username = username
            };

            _todoService.Create(newTask);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var username = GetCurrentUsername();
            if (username == null) return RedirectToAction("Login", "Account");

            var todo = _todoService.GetById(id);
            if (todo == null || todo.Username != username)
                return NotFound();

            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(string id, string title)
        {
            var username = GetCurrentUsername();
            if (username == null) return RedirectToAction("Login", "Account");

            var todo = _todoService.GetById(id);
            if (todo == null || todo.Username != username)
                return NotFound();

            todo.Title = title;
            _todoService.Update(id, todo);
            return RedirectToAction("Index");
        }

        public IActionResult Toggle(string id)
        {
            var username = GetCurrentUsername();
            if (username == null) return RedirectToAction("Login", "Account");

            var todo = _todoService.GetById(id);
            if (todo == null || todo.Username != username)
                return NotFound();

            todo.IsDone = !todo.IsDone;
            _todoService.Update(id, todo);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var username = GetCurrentUsername();
            if (username == null) return RedirectToAction("Login", "Account");

            var todo = _todoService.GetById(id);
            if (todo == null || todo.Username != username)
                return NotFound();

            _todoService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
