using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FindMyAdvisor.Models;

namespace FindMyAdvisor.Dtos
{
    public class ProfessorDto
    {
        public int? Id { get; set; }

        [Display(Name = "Professor Name")]
        public string Name { get; set; }

        [Display(Name = "Year Join")]
        public DateTime? Join_Date { get; set; }

        [StringLength(50)]
        public string Homepage { get; set; }

        [StringLength(50)]
        public string Photo_Link { get; set; }

        [ForeignKey("University")]
        public int? UniversityId { get; set; }
        public virtual University University { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("GraduateFrom")]
        [Display(Name = "Graduate From")]
        public int? GraduateFromId { get; set; }
        public virtual University GraduateFrom { get; set; }

        public int? RankId { get; set; }
        public virtual Rank Rank { get; set; }

        public int? ResearchId { get; set; }
        public virtual Research Research { get; set; }

        public int? Likes { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}