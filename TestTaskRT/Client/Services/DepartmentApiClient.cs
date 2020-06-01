using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Radzen;
using TestTaskRT.Shared.DAL;

namespace TestTaskRT.Client.Services
{
    public class DepartmentApiClient : IDepartmentApiClient
    {
        private readonly HttpClient _client = new HttpClient();
        private const string ControllerEntryPoint = "departments";

        public DepartmentApiClient(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri(configuration.GetSection("Endpoints")["DepartmentsEndpoint"]);
        }

        public async Task<(bool, int)> CreateDepartment(DepartmentModel model)
        {
            var response = await _client.PostAsync(ControllerEntryPoint, JsonContent.Create(model));
            return (response.IsSuccessStatusCode, await response.ReadAsync<int>());
        }

        public Task<List<DepartmentModel>> GetDepartments() =>
            _client.GetFromJsonAsync<List<DepartmentModel>>(ControllerEntryPoint);

        public async Task<bool> DeleteDepartment(int departmentId) =>
            (await _client.DeleteAsync($"{ControllerEntryPoint}?departmentid={departmentId}")).IsSuccessStatusCode;

        public async Task<bool> UpdateDepartment(DepartmentModel model) =>
            (await _client.PatchAsync(ControllerEntryPoint, JsonContent.Create(model))).IsSuccessStatusCode;
    }
}