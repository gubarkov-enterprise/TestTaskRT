using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskRT.Shared.DAL;

namespace TestTaskRT.Client.Services
{
    public interface IUsersApiClient
    {
        Task<List<UserModel>> GetUsers();
        Task<bool> CreateUser(CreateUserModel model);
        Task<bool> DeleteUser(int id);
    }
}