using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskRT.Shared;
using TestTaskRT.Shared.DAL;
using TestTaskRT.Shared.Dbo.Entities;

namespace DepartmentsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentModel>> Get() => await
            _context.Departments
                .Select(entity => new DepartmentModel
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Users = entity.Users.Select(userEntity => new UserModel
                        {Id = userEntity.Id, Name = userEntity.Name, Surname = userEntity.Surname}).ToList()
                })
                .ToListAsync();

        [HttpPost]
        public Task<int> Create([FromBody] DepartmentModel model)
        {
            _context.Departments.Add(new DepartmentEntity {Title = model.Title});
            return _context.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task Delete([FromQuery] int departmentId)
        {
            var existing = await _context.Departments.FirstOrDefaultAsync(entity => entity.Id == departmentId);
            if (existing == null)
            {
                BadRequest("Department does not exist");
            }

            _context.Departments.Remove(existing!);
            await _context.SaveChangesAsync();
        }

        [HttpPatch]
        public async Task Delete([FromBody] DepartmentModel model)
        {
            var existing = await _context.Departments.FirstOrDefaultAsync(entity => entity.Id == model.Id);
            if (existing == null)
            {
                BadRequest("Department does not exist");
            }

            existing!.Title = model.Title;
            await _context.SaveChangesAsync();
        }
    }
}