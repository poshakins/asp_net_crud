using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAdminPortal.Data;
using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Models.DTO;

namespace EmployeeAdminPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext; //to read what is in the database privately
        //connect to database using db context to get data using constructor
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            return Ok(allEmployees);
        }
        

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
             // Check if an employee with the same email already exists
            var existingEmployee = dbContext.Employees.FirstOrDefault(e => e.Email == addEmployeeDto.Email);
            if (existingEmployee != null)
            {
                return Conflict(new { Message = "An employee with this email already exists." });
            }
            // Create a new Employee entity from the DTO
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };

            // Add the entity to the database
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            // Return a 201 Created response with the newly created entity
            return CreatedAtAction(nameof(GetAllEmployees), new { id = employeeEntity.Id }, employeeEntity);
        }

        //get employee by id
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployById (Guid id)
        {   
         //check if employee exists
         var existingEmployeeId = dbContext.Employees.Find(id);
         if (existingEmployeeId is null)
         {
            // Return 404 if the employee doesn't exist
            return NotFound(new { Message = "Employee not found." });
         }
            // Return the employee details
          return Ok(existingEmployeeId );
        }

    }
}