using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FindMyAdvisor.Models;

namespace FindMyAdvisor.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Display Name")]
        [Index(IsUnique = true)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<Professor> Professors { get; set; }

        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}