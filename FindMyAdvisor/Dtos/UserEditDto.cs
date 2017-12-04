using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindMyAdvisor.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyAdvisor.Dtos
{
    public class UserEditDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}