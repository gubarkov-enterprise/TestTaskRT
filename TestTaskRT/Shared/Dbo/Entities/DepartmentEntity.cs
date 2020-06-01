using System.Collections.Generic;

namespace TestTaskRT.Shared.Dbo.Entities
{
    public class DepartmentEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}