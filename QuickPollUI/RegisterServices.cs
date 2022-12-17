
namespace QuickPollUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
        builder.Services.AddControllersWithViews()
            .AddMicrosoftIdentityUI();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireClaim("JobTitle", "Admin");
            });
        });

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor()
            .AddMicrosoftIdentityConsentHandler();

        builder.Services.AddSingleton<IMongoDbConnection, MongoDbConnection>();
        builder.Services.AddSingleton<IMongoUserData, MongoUserData>();
        builder.Services.AddSingleton<IMongoPollData, MongoPollData>();
        builder.Services.AddSingleton<IPollService, PollService>();
        builder.Services.AddSingleton<IChatService, ChatService>();
    }
}
