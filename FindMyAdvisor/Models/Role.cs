using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.Models
{
    public class Role
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Role")]
        [StringLength(16)]
        [Index(IsUnique = true)]
        public string RoleName { get; set; }
    }
}