using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.Models
{
    public class Professor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Display(Name = "Year Join")]
        public DateTime Join_Date { get; set; }

        [StringLength(150)]
        public string Homepage { get; set; }

        [StringLength(150)]
        public string Photo_Link { get; set; }

        [ForeignKey("University")]
        public int UniversityId { get; set; }
        public virtual University University { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("Bachelor")]
        public int? BachelorId { get; set; }
        public virtual University Bachelor { get; set; }
        
        [ForeignKey("Master")]
        public int? MasterId { get; set; }
        public virtual University Master { get; set; }

        [ForeignKey("PhD")]
        public int? PhdId { get; set; }
        public virtual University PhD { get; set; }

        public int RankId { get; set; }
        public virtual Rank Rank { get; set; }

        public int ResearchId { get; set; }
        public virtual Research Research { get; set; }

        public int? Likes { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}