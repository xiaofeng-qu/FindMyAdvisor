using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.Models
{
    public class University
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="University Name")]
        public string Name { get; set; }
    }
}