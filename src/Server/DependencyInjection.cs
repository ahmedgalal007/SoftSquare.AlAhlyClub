using Blazor.Analytics;
using SoftSquare.AlAhlyClub.Infrastructure.Configurations;
using SoftSquare.AlAhlyClub.Infrastructure.Constants.Localization;
using SoftSquare.AlAhlyClub.Server.Middlewares;
using SoftSquare.AlAhlyClub.Server.Services;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SoftSquare.AlAhlyClub.Server;

public static class DependencyInjection
{
    public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<LocalizationCookiesMiddleware>()
            .Configure<RequestLocalizationOptions>(options =>
            {
                options.AddSupportedUICultures(LocalizationConstants.SupportedLanguages.Select(x => x.Code).ToArray());
                options.AddSupportedCultures(LocalizationConstants.SupportedLanguages.Select(x => x.Code).ToArray());
                options.FallBackToParentUICultures = true;
            })
            .AddLocalization(options => options.ResourcesPath = LocalizationConstants.ResourcesPath);

        services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseInMemoryStorage())
            .AddHangfireServer()
            .AddMvc();

        services.AddControllers();

        services.AddScoped<IApplicationHubWrapper, ServerHubWrapper>()
            .AddSignalR();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddHealthChecks();

        var privacySettings = config.GetRequiredSection(PrivacySettings.Key).Get<PrivacySettings>();
        if (privacySettings!.UseGoogleAnalytics)
        {
            if (privacySettings.GoogleAnalyticsKey is null or "")
                throw new ArgumentNullException(nameof(privacySettings.GoogleAnalyticsKey));

            services.AddGoogleAnalytics(privacySettings.GoogleAnalyticsKey);
        }

        return services;
    }
}