using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KanbanBoard.Models;

namespace KanbanBoard.Controllers
{
    public class HomeController : Controller
    {
        // главная страница: доска
        public IActionResult Index()
        {
            return View();
        }
        // страница ошибки
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}