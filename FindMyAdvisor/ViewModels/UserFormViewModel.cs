using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindMyAdvisor.Models;

namespace FindMyAdvisor.ViewModels
{
    public class UserFormViewModel
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }
    }
}