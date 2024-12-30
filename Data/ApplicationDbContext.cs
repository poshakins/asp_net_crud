using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        //create constructor for the database
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {
             
        }

        public DbSet<Employee> Employees { get; set; }
    }
}