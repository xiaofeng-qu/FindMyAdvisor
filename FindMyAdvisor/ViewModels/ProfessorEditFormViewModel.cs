using FindMyAdvisor.Models;
using FindMyAdvisor.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FindMyAdvisor.ViewModels
{
    public class ProfessorEditFormViewModel
    {
        public Professor Professor { get; set; }
        public IEnumerable<Rank> Ranks { get; set; }
    }
}