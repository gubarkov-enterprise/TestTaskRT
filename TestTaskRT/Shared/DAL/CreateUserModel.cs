using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskRT.Shared.DAL
{
    public class CreateUserModel
    {
        public int DepartmentId { get; set; }
        public UserModel User { get; set; } = new UserModel();
    }
}