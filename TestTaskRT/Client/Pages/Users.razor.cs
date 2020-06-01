using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using TestTaskRT.Client.Services;
using TestTaskRT.Shared.DAL;

namespace TestTaskRT.Client.Pages
{
    public class UsersComponent : ComponentBase, IDisposable
    {
        [Inject] protected IUsersApiClient UsersApiClient { get; set; }
        [Inject] protected DialogService DialogService { get; set; }

        protected RadzenGrid<UserModel> Grid { get; set; }

        protected List<UserModel> Users { get; set; }

        protected bool IsInitialized;

        protected override async Task OnInitializedAsync()
        {
            DialogService.OnClose += o =>
            {
                if (o is UserModel model)
                    Users.Add(model);
                InvokeAsync(StateHasChanged);
            };
            await LoadData();
        }

        private async Task LoadData()
        {
            Users = new List<UserModel>(await UsersApiClient.GetUsers());
            IsInitialized = true;
        }

        public void Dispose()
        {
            DialogService?.Dispose();
        }
    }
}