using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using intro_to_mvc6.Models;
using intro_to_mvc6.ActionFilters;
using Microsoft.AspNet.Authorization;

namespace intro_to_mvc6.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationRepository _repository;

        public HomeController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [ImportModelStateFromTempData, Authorize]
        public IActionResult Index()
        {
            return View(GetToDos());
        }

        [HttpPost, ExportModelStateToTempData]
        public IActionResult Create(ViewModels.ToDoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!_repository.AddToDo(AutoMapper.Mapper.Map(viewModel, new ToDo())))
                {
                    ModelState.AddModelError(string.Empty, "There was an error saving the todo to the database.");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ExportModelStateToTempData]
        public IActionResult Update(ViewModels.ToDoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!_repository.UpdateToDo(AutoMapper.Mapper.Map(viewModel,new ToDo())))
                {
                    ModelState.AddModelError(string.Empty, "There was an error saving the todo to the database.");
                }
            }

            return RedirectToAction("Index");
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

        private List<ViewModels.ToDoViewModel> GetToDos()
        {
            var todos = _repository.GetAllToDos();
            return AutoMapper.Mapper.Map(todos, new List<ViewModels.ToDoViewModel>());
        }
    }
}
