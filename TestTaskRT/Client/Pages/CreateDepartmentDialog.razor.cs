using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using TestTaskRT.Client.Services;
using TestTaskRT.Shared.DAL;

namespace TestTaskRT.Client.Pages
{
    public class CreateDepartmentDialogComponent : ComponentBase
    {
        public DepartmentModel NewDepartment = new DepartmentModel();
        [Inject] protected DialogService DialogService { get; set; }
        [Inject] protected IDepartmentApiClient DepartmentApiClient { get; set; }
        [Inject] protected NotificationService NotificationService { get; set; }

        protected bool IsResponseAwaiting;

        protected async Task Submit(DepartmentModel model)
        {
            IsResponseAwaiting = true;
            var result = await DepartmentApiClient.CreateDepartment(model);
            if (result.isSuccess)
                NotificationService.Notify(new NotificationMessage
                {
                    Duration = 5000, Severity = NotificationSeverity.Success,
                    Summary = "Department created"
                });
            IsResponseAwaiting = false;
            DialogService.Close(new DepartmentModel
            {
                Id = result.Id, Title = model.Title, Users = new List<UserModel>()
            });
        }

        protected void Cancel() => DialogService.Close();
    }
}