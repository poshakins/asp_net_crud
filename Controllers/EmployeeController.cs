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

            // Create a response object with the message and the employee data
            var response = new
            {
                Message = "Employee created successfully.",
                Employee = employeeEntity
            };

            // Return a 201 Created response with the newly created entity
            // return CreatedAtAction(nameof(GetAllEmployees), new { id = employeeEntity.Id }, employeeEntity);
            return CreatedAtAction(nameof(GetAllEmployees), new { id = employeeEntity.Id }, response);
        }

        //get employee by id
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById (Guid id)
        {   
         //check if employee exists
         var existingEmployeeId = dbContext.Employees.Find(id);
         if (existingEmployeeId is null)
         {
            // Return 404 if the employee doesn't exist
            return NotFound(new { Message = "Employee not found." });
         }
            // Return the employee details
          return Ok(existingEmployeeId);
        }

        // update the employee
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id,UpdateEmployeeDto updateEmployeeDto)
        {
            //check if employee exists
            var employeeToUpdate = dbContext.Employees.Find(id);
            if (employeeToUpdate is null)
            {
                // Return 404 if the employee doesn't exist
                return NotFound(new { Message = "Employee not found." });
            }  
            //update the data if found
            employeeToUpdate.Name = updateEmployeeDto.Name;
            employeeToUpdate.Phone = updateEmployeeDto.Phone;
            employeeToUpdate.Salary = updateEmployeeDto.Salary;
            //save changes made to the employee
            dbContext.SaveChanges(); 
            // Return a response with a success message and the updated employee data
            return Ok(new
            {
                Message = "Employee updated successfully.",
                Employee = employeeToUpdate
            });
        }


        //delete an employee from the database
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployeeById (Guid id)
        {   
            //check if employee exists
            var employeeIdToDelete = dbContext.Employees.Find(id);
            if (employeeIdToDelete is null)
            {
                // Return 404 if the employee doesn't exist
                return NotFound(new { Message = "Employee not found." });
            }
            //delete the employee entity
            dbContext.Employees.Remove(employeeIdToDelete);

            //save changes made to the employee
            dbContext.SaveChanges(); 
            // Return a response with a success message and the deleted employee data
            return Ok(new
            {
                Message = "Employee deleted successfully.",
                Employee = employeeIdToDelete
            });
            
        
        }

    }
}