using TestTaskRT.Shared.Dbo.Entities;


namespace TestTaskRT.Shared.DAL
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DepartmentModel Department { get; set; }

        public static explicit operator UserModel(UserEntity entity)
            => new UserModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Department = (DepartmentModel) entity.Department
            };
    }
}