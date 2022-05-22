using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SportMeetingsApi.Shared.Services;

namespace SportMeetingsApi.Shared;

public static class ComponentsRegistration {
    public static void RegisterSharedComponents(this IServiceCollection serviceCollection) {
        serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        serviceCollection.AddScoped<IContext, HttpCurrentContext>();
    }
}
