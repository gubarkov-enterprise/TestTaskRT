using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using TestTaskRT.Client.Services;
using TestTaskRT.Shared.DAL;

namespace TestTaskRT.Client.Pages
{
    public class CreateUserDialogComponent : ComponentBase
    {
        [Inject] protected IUsersApiClient UsersApiClient { get; set; }
        [Inject] protected IDepartmentApiClient DepartmentsApiClient { get; set; }
        [Inject] protected DialogService DialogService { get; set; }
        [Inject] protected NotificationService NotificationService { get; set; }

        protected CreateUserModel NewUser = new CreateUserModel();
        protected IEnumerable<DepartmentModel> Departments;
        protected bool Awaiting;
        protected bool DepartmentsLoaded;

        protected override async Task OnInitializedAsync()
        {
            await LoadDepartments();
        }

        private async Task LoadDepartments()
        {
            Departments = await DepartmentsApiClient.GetDepartments();
            DepartmentsLoaded = true;
        }

        protected void Change(object value)
        {
            NewUser.DepartmentId = (int) value;
        }

        protected async Task Submit(CreateUserModel model)
        {
            Awaiting = true;
            var result = await UsersApiClient.CreateUser(model);
            if (result)
                NotificationService.Notify(new NotificationMessage
                {
                    Duration = 5000,
                    Severity = NotificationSeverity.Success,
                    Summary = "User created"
                });
            Awaiting = false;
            var title = Departments.FirstOrDefault(departmentModel => departmentModel.Id == NewUser.DepartmentId)
                ?.Title;

            DialogService.Close(new UserModel
            {
                Name = model.User.Name,
                Surname = model.User.Surname,
                Department = new DepartmentModel
                {
                    Title = title,
                    Id = NewUser.DepartmentId,
                    Users = new List<UserModel>()
                }
            });
        }

        protected void Cancel() => DialogService.Close();
    }
}