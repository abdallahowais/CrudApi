using CrudApi.data;
using CrudApi.DTOs.Employees;
using CrudApi.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("Getall")]
        public IActionResult Getall() {
            
            var employees =context.Employees.ToList();
            var response = employees.Adapt<IEnumerable<GetEmployeesDto>>();
            return Ok(response);
        }


        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var employee = context.Employees.Find(id);
            if( employee is null)
            {
                return NotFound("employee not found");
            };

            var response = employee.Adapt<GetEmployeesDto>();

            return Ok(response);
        }


        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeeDto empdto)
        {
            var employee = empdto.Adapt<Employee>();
           context.Employees.Add(employee);
            context.SaveChanges();  
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, UpdateEmployeeDto updateDto)
        {
            var current = context.Employees.Find(id);
            if (current is null)
            {
                return NotFound("employee not found");
            }
            updateDto.Adapt(current);
            context.SaveChanges();

            var response = current.Adapt<GetEmployeesDto>();
            return Ok(response);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("employee not found");
            }
            context.Employees.Remove(employee);
            context.SaveChanges();

            var response = employee.Adapt<GetEmployeesDto>();

            return Ok(response);
        }

    }
}
