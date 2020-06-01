using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TestTaskRT.Shared;
using TestTaskRT.Shared.DAL;

namespace TestTaskRT.Client.Services
{
    public class UsersApiClient : IUsersApiClient
    {
        private readonly HttpClient _client = new HttpClient();
        private const string ControllerEntryPoint = "users";


        public UsersApiClient(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri(configuration.GetSection("Endpoints")["UsersEndpoint"]);
        }

        public async Task<List<UserModel>> GetUsers() =>
            await _client.GetFromJsonAsync<List<UserModel>>(ControllerEntryPoint);

        public async Task<bool> CreateUser(CreateUserModel model) =>
            (await _client.PostAsync(ControllerEntryPoint, JsonContent.Create(model))).IsSuccessStatusCode;

        public async Task<bool> DeleteUser(int id) =>
            (await _client.DeleteAsync($"{ControllerEntryPoint}?userId={id}")).IsSuccessStatusCode;
    }
}