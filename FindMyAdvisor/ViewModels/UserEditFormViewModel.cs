using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FindMyAdvisor.Models;
using System.ComponentModel.DataAnnotations.Schema;
using FindMyAdvisor.Dtos;

namespace FindMyAdvisor.ViewModels
{
    public class UserEditFormViewModel
    {
        public UserEditDto User { get; set; }
        public List<Role> Roles { get; set; }
    }
}