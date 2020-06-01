using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskRT.Shared.Dbo.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual DepartmentEntity Department { get; set; } = new DepartmentEntity();
    }
}