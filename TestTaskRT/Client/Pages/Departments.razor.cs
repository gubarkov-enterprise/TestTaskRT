using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using TestTaskRT.Client.Services;
using TestTaskRT.Shared.DAL;

namespace TestTaskRT.Client.Pages
{
    public class DepartmentsComponent : ComponentBase, IDisposable
    {
        [Inject] private IDepartmentApiClient DepartmentApiClient { get; set; }
        [Inject] private IUsersApiClient UsersApiClient { get; set; }

        [Inject] protected DialogService DialogService { get; set; }
        [Inject] protected NotificationService NotificationService { get; set; }

        protected RadzenGrid<DepartmentModel> DepartmentsGrid;
        protected List<DepartmentModel> Departments { get; set; }


        protected bool IsInitialized;


        protected override async Task OnInitializedAsync()
        {
            DialogService.OnClose += o =>
            {
                if (o is DepartmentModel model)
                    Departments.Add(model);
                InvokeAsync(StateHasChanged);
            };
            await LoadData();
        }

        private async Task LoadData()
        {
            Departments = await DepartmentApiClient.GetDepartments();
            IsInitialized = true;
        }

        protected async Task DeleteDepartment(DepartmentModel model)
        {
            var result = await DepartmentApiClient.DeleteDepartment(model.Id);
            if (result)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Summary = "Department deleted",
                    Duration = 5000,
                    Severity = NotificationSeverity.Info
                });
                Departments = Departments.Where(departmentModel => departmentModel.Id != model.Id).ToList();
                await InvokeAsync(StateHasChanged);
            }
        }

        protected void EditRow(DepartmentModel model)
        {
            DepartmentsGrid.EditRow(model);
        }

        protected async Task SaveRow(DepartmentModel model)
        {
            var result = await DepartmentApiClient.UpdateDepartment(model);
            if (result)
                NotificationService.Notify(new NotificationMessage
                {
                    Summary = "Department updated",
                    Duration = 5000,
                    Severity = NotificationSeverity.Success
                });
            await DepartmentsGrid.UpdateRow(model);
        }

        protected async Task CancelEdit(DepartmentModel model) => DepartmentsGrid.CancelEditRow(model);

        protected async Task DeleteUser(int userId, DepartmentModel department)
        {
            var result = await UsersApiClient.DeleteUser(userId);
            if (result)
            {
                var user = department.Users.Find(model => model.Id == userId);
                NotificationService.Notify(new NotificationMessage
                {
                    Summary = $"{user.Name} {user.Surname} deleted",
                    Duration = 5000,
                    Severity = NotificationSeverity.Info
                });
                department.Users.Remove(user);
                await InvokeAsync(StateHasChanged);
            }
        }

        public void Dispose()
        {
            DialogService?.Dispose();
        }
    }
}