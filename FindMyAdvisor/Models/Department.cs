using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Department Name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}