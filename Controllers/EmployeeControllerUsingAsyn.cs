// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using EmployeeAdminPortal.Data;
// using Microsoft.AspNetCore.Mvc;
// using EmployeeAdminPortal.Models.Entities;
// using EmployeeAdminPortal.Models.DTO;

// namespace EmployeeAdminPortal.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class EmployeeController : ControllerBase
//     {
//         private readonly ApplicationDbContext dbContext;

//         public EmployeeController(ApplicationDbContext dbContext)
//         {
//             this.dbContext = dbContext;
//         }

//         // Get all employees
//         [HttpGet]
//         public async Task<IActionResult> GetAllEmployees()
//         {
//             var allEmployees = await Task.Run(() => dbContext.Employees.ToList());
//             return Ok(allEmployees);
//         }

//         // Add an employee
//         [HttpPost]
//         public async Task<IActionResult> AddEmployee(AddEmployeeDto addEmployeeDto)
//         {
//             // Check if an employee with the same email already exists
//             var existingEmployee = await Task.Run(() => dbContext.Employees.FirstOrDefault(e => e.Email == addEmployeeDto.Email));
//             if (existingEmployee != null)
//             {
//                 return Conflict(new { Message = "An employee with this email already exists." });
//             }

//             // Create a new Employee entity from the DTO
//             var employeeEntity = new Employee
//             {
//                 Name = addEmployeeDto.Name,
//                 Email = addEmployeeDto.Email,
//                 Phone = addEmployeeDto.Phone,
//                 Salary = addEmployeeDto.Salary,
//             };

//             dbContext.Employees.Add(employeeEntity);
//             await dbContext.SaveChangesAsync();

//             var response = new
//             {
//                 Message = "Employee created successfully.",
//                 Employee = employeeEntity
//             };

//             return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeEntity.Id }, response);
//         }

//         // Get an employee by ID
//         [HttpGet]
//         [Route("{id:guid}")]
//         public async Task<IActionResult> GetEmployeeById(Guid id)
//         {
//             var existingEmployeeId = await dbContext.Employees.FindAsync(id);
//             if (existingEmployeeId == null)
//             {
//                 return NotFound(new { Message = "Employee not found." });
//             }

//             return Ok(existingEmployeeId);
//         }

//         // Update an employee
//         [HttpPut]
//         [Route("{id:guid}")]
//         public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
//         {
//             var employeeToUpdate = await dbContext.Employees.FindAsync(id);
//             if (employeeToUpdate == null)
//             {
//                 return NotFound(new { Message = "Employee not found." });
//             }

//             employeeToUpdate.Name = updateEmployeeDto.Name;
//             employeeToUpdate.Phone = updateEmployeeDto.Phone;
//             employeeToUpdate.Salary = updateEmployeeDto.Salary;

//             await dbContext.SaveChangesAsync();

//             return Ok(new
//             {
//                 Message = "Employee updated successfully.",
//                 Employee = employeeToUpdate
//             });
//         }

//         // Delete an employee by ID
//         [HttpDelete]
//         [Route("{id:guid}")]
//         public async Task<IActionResult> DeleteEmployeeById(Guid id)
//         {
//             var employeeIdToDelete = await dbContext.Employees.FindAsync(id);
//             if (employeeIdToDelete == null)
//             {
//                 return NotFound(new { Message = "Employee not found." });
//             }

//             dbContext.Employees.Remove(employeeIdToDelete);
//             await dbContext.SaveChangesAsync();

//             return Ok(new
//             {
//                 Message = "Employee deleted successfully.",
//                 Employee = employeeIdToDelete
//             });
//         }
//     }
// }
