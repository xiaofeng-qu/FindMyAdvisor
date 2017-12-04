using FindMyAdvisor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.ViewModels
{
    public class ProfessorDetailViewModel
    {
        public Professor Professor { get; set; }
        public List<Professor> Professors { get; set; }
    }
}