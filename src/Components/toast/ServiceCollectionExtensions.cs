using Blazored.Toast.Services;
using Microsoft.Extensions.DependencyInjection;

namespace de.springwald.blazortools.Components.toast
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazoredToast(this IServiceCollection services)
        {
            return services.AddScoped<IToastService, ToastService>();
        }
    }
}
