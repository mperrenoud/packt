using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intro_to_mvc6.Models
{
    public class ApplicationRepository : IApplicationRepository
    {
        private ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ToDo> GetAllToDos()
        {
            return _context.ToDos.ToList();
        }

        public bool AddToDo(ToDo model)
        {
            _context.ToDos.Add(model);
            return _context.SaveChanges() == 1;
        }
    }

    public interface IApplicationRepository
    {
        List<ToDo> GetAllToDos();

        bool AddToDo(ToDo model);
    }
}
