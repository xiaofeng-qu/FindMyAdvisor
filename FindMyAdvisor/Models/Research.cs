
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.Models
{
    public class Research
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name ="Research Interest")]
        [Index(IsUnique = true)]
        public string Research_Interest { get; set; }
    }
}