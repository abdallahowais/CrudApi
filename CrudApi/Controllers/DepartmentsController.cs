using CrudApi.data;
using CrudApi.DTOs.Department;
using CrudApi.DTOs.Departments;
using CrudApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("Getall")]
        public IActionResult Getall()
        {

            var departments = context.Departments.Select(
                
                DepDto=>new GetDepartmentsDto()
                {
                    Id = DepDto.Id,
                    Name = DepDto.Name,
                }
                );
            return Ok(departments);
        }




        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var departments = context.Departments.Find(id);
            if (departments is null)
            {
                return NotFound("departments not found");
            };
            var result = new GetDepartmentsDto()
            {
                Id = departments.Id,
                Name = departments.Name
            };

            return Ok(result);
        }


        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDto depdto)
        {
            Department dep = new Department()
            {
                Name = depdto.Name
            };
            context.Departments.Add(dep);
            context.SaveChanges();

            return Ok(dep);
        }



        [HttpPut("Update")]
        public IActionResult Update(int id, UpdateDepartmentDto updateDto)
        {
            var current = context.Departments.Find(id);
            if (current is null)
            {
                return NotFound("department not found");
            }
            current.Name = updateDto.Name;
            context.SaveChanges();

            var result = new GetDepartmentsDto()
            {
                Id = current.Id,
                Name = current.Name
            };

            return Ok(result);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var department = context.Departments.Find(id);
            if (department is null)
            {
                return NotFound("employee not found");
            }
            context.Departments.Remove(department);
            context.SaveChanges();

            var result = new GetDepartmentsDto()
            {
                Id = department.Id,
                Name = department.Name
            };

            return Ok(result);
        }
    }
        
    }
