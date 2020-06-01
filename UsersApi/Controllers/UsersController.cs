using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskRT.Shared;
using TestTaskRT.Shared.DAL;
using TestTaskRT.Shared.Dbo.Entities;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> Get()
            => await _context.Users
                .Select(entity => new UserModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    Department = new DepartmentModel {Id = entity.DepartmentId, Title = entity.Department.Title}
                })
                .ToListAsync();


        [HttpPost]
        public async Task<int> Create([FromBody] CreateUserModel model)
        {
            var existingDepartment =
                await _context.Departments.FirstOrDefaultAsync(entity => entity.Id == model.DepartmentId);

            if (existingDepartment == null)
            {
                BadRequest("Department does not exist");
            }

            await _context.Users.AddAsync(new UserEntity
                {Name = model.User.Name, Surname = model.User.Surname, Department = existingDepartment});
            return await _context.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task Delete([FromQuery] int userId)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(entity => entity.Id == userId);
            if (existingUser == null)
            {
                BadRequest("User does not exist");
            }

            _context.Users.Remove(existingUser!);
            await _context.SaveChangesAsync();
        }
    }
}