using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindMyAdvisor.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FindMyAdvisor.Models;

namespace FindMyAdvisor.Dtos
{
    public class ProfessorDtoForEditForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Display(Name = "Year Join")]
        public DateTime Join_Date { get; set; }

        [StringLength(50)]
        public string Homepage { get; set; }

        [StringLength(50)]
        public string Photo_Link { get; set; }

        [ForeignKey("University")]
        public int UniversityId { get; set; }
        public virtual University University { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("Bachelor")]
        public int? BachelorId { get; set; }
        public virtual UniversityDto Bachelor { get; set; }

        [ForeignKey("Master")]
        public int? MasterId { get; set; }
        public virtual UniversityDto Master { get; set; }

        [ForeignKey("PhD")]
        public int? PhdId { get; set; }
        public virtual UniversityDto PhD { get; set; }

        public int RankId { get; set; }
        public virtual Rank Rank { get; set; }

        public int ResearchId { get; set; }
        public virtual Research Research { get; set; }

        public int? Likes { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}