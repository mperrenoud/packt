using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using intro_to_mvc6.Models;

namespace intro_to_mvc6.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationRepository _repository;

        public HomeController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var todos = _repository.GetAllToDos();
            return View(AutoMapper.Mapper.Map(todos, new List<ViewModels.ToDoViewModel>()));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
