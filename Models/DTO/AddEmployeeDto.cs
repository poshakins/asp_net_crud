using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Models.DTO
{
    public class AddEmployeeDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; } //add ? to make it a nullable property
        public decimal Salary { get; set; }  
    }
}