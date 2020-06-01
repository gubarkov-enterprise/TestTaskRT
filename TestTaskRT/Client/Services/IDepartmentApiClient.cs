using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskRT.Shared.DAL;

namespace TestTaskRT.Client.Services
{
    public interface IDepartmentApiClient
    {
        Task<(bool isSuccess, int Id)> CreateDepartment(DepartmentModel model);
        Task<List<DepartmentModel>> GetDepartments();
        Task<bool> DeleteDepartment(int departmentId);
        Task<bool> UpdateDepartment(DepartmentModel model);
    }
}
