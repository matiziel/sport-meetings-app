using Microsoft.Extensions.DependencyInjection;
using SportMeetingsApi.SportEvents.Events.Query;

namespace SportMeetingsApi.SportEvents;

public static class ComponentsRegistration {
    public static void RegisterSportEventsComponents(this IServiceCollection serviceCollection) {
        serviceCollection.AddScoped<SportEventsQueryService>();
    }
}
