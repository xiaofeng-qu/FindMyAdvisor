using FindMyAdvisor.Models;
using FindMyAdvisor.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FindMyAdvisor.ViewModels
{
    public class ProfessorFormViewModel
    {
        public IEnumerable<Rank> Ranks { get; set; }
        public ProfessorDto Professor { get; set; }
    }
}