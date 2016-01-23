using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intro_to_mvc6.ViewModels
{
    public class ToDoViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
