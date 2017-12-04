using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FindMyAdvisor.Models;

namespace FindMyAdvisor.Context
{
    public class FindMyAdvisorDBInitializer : DropCreateDatabaseAlways<FindMyAdvisorContext>
    {
        protected override void Seed(FindMyAdvisorContext _context)
        {
            IList<Role> defaultRoles = new List<Role>()
            {
                new Role() { RoleName = "admin" },
                new Role() { RoleName = "user" }
            };

            IList<Department> defaultDepartments = new List<Department>()
            {
                new Department() { Name = "Computer Science" }
            };

            IList<Rank> defaultRanks = new List<Rank>()
            {
                new Rank() { Name = "Full" },
                new Rank() { Name = "Associate" },
                new Rank() { Name = "Assistant" }
            };

            IList<User> defaultUsers = new List<User>()
            {
                new User()
                {
                    DisplayName = "Xiaofeng Admin",
                    Email = "xiaofeng@live.com",
                    Password = "Agoodday",
                    ConfirmPassword = "Agoodday",
                    RoleId = 1
                },

                new User()
                {
                    DisplayName = "Xiaofeng User",
                    Email = "xiaofeng2@live.com",
                    Password = "Agoodday",
                    ConfirmPassword = "Agoodday",
                    RoleId = 2
                }
            };

            foreach (var role in defaultRoles) _context.Roles.Add(role);
            foreach (var dept in defaultDepartments) _context.Departments.Add(dept);
            foreach (var rank in defaultRanks) _context.Ranks.Add(rank);
            foreach (var user in defaultUsers) _context.Users.Add(user);
            base.Seed(_context);
        }
    }
}