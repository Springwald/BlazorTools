using de.springwald.blazortools.Components.toast;
using de.springwald.blazortools.Services.twaddle;
using de.springwald.blazortools.Services.waiting;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace de.springwald.blazortools.test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IWaitingService, WaitingService>();

            builder.Services.AddBlazoredToast();
            builder.Services.AddScoped<ITwaddleService, TwaddleService>();

            await builder.Build().RunAsync();
        }
    }
}
