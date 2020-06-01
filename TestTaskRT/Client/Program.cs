using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radzen;
using TestTaskRT.Client.Services;

namespace TestTaskRT.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient
                {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddTransient<IUsersApiClient, UsersApiClient>();
            builder.Services.AddTransient<IDepartmentApiClient, DepartmentApiClient>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();


            await builder.Build().RunAsync();
        }
    }
}