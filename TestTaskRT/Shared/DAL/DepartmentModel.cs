using System.Collections.Generic;
using System.Linq;
using TestTaskRT.Shared.Dbo.Entities;

namespace TestTaskRT.Shared.DAL
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<UserModel> Users { get; set; } = new List<UserModel>();

        public static explicit operator DepartmentModel(DepartmentEntity entity)
            => new DepartmentModel
            {
                Id = entity.Id,
                Title = entity.Title,
            };
    }
}