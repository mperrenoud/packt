using intro_to_mvc6.Models;
using intro_to_mvc6.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intro_to_mvc6.Controllers.API
{
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private IApplicationRepository _repository;

        public ToDoController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public JsonResult Get()
        {
            var todos = AutoMapper.Mapper.Map(this._repository.GetAllToDos(), new List<ToDoViewModel>());
            return Json(todos);
        }
    }
}
