using FindMyAdvisor.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.ViewModels
{
    public class ProfessorSearchResultViewModel
    {
        public IEnumerable<Rank> Ranks { get; set; }

        [Display(Name = "Professor Name")]
        public string Name { get; set; }

        [Display(Name = "Year Join")]
        public DateTime? Join_Date { get; set; }

        public string University { get; set; }

        public string Department { get; set; }

        [Display(Name = "Graduate Frome")]
        public string GraduateFrom { get; set; }

        public int? RankId { get; set; }

        [Display(Name = "Research Interest")]
        public string ResearchInterest { get; set; }

        public IPagedList<Professor> SearchResults { get; set; }
    }
}