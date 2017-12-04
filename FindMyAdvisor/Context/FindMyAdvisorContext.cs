using FindMyAdvisor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace FindMyAdvisor.Context
{
    public class FindMyAdvisorContext : DbContext
    {
        public FindMyAdvisorContext() : base("name=DefaultConnection")
        {
            
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}