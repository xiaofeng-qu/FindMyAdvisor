using FindMyAdvisor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.Context
{
    public class FindMyAdvisorContext : DbContext
    {
        public FindMyAdvisorContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<Rank> Ranks { get; set; }

    }
}