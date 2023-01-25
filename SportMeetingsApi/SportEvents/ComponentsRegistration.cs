using Microsoft.Extensions.DependencyInjection;
using SportMeetingsApi.SportEvents.Events.Command;
using SportMeetingsApi.SportEvents.Events.Query;
using SportMeetingsApi.SportEvents.SignUps.Command;
using SportMeetingsApi.SportEvents.SignUps.Query;

namespace SportMeetingsApi.SportEvents;

public static class ComponentsRegistration {
    public static void RegisterSportEventsComponents(this IServiceCollection serviceCollection) {
        serviceCollection.AddScoped<SignUpsQueryService>();
        serviceCollection.AddScoped<SignUpsService>();
        serviceCollection.AddScoped<SportEventsQueryService>();
        serviceCollection.AddScoped<SportEventsService>();

    }
}
