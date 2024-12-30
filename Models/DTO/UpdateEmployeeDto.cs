using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Models.DTO
{
    public class UpdateEmployeeDto
    {
        public required string Name { get; set; }
        public string? Phone { get; set; } //add ? to make it a nullable property
        public decimal Salary { get; set; } 
    }
}