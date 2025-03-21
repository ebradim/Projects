using System.Net.Mime;
using CustomTicketStore.Clients.CMSApp.Filters;
using CustomTicketStore.Shared.Infrastructures.CQRS;
using CustomTicketStore.Shared.Infrastructures.WebAPI;
using CustomTicketStore.Shared.Infrastructures.WebAPI.UserAccessor;
using CustomTicketStore.Shared.Modules.Accounts;
using CustomTicketStore.Shared.Infrastructures.Persistence;
using CustomTicketStore.Shared.Infrastructures.WebAPI.SessionStore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);
var postgresUri = builder.Configuration.GetConnectionString("Postgres");
builder.Logging.AddConsole();
ArgumentNullException.ThrowIfNull(postgresUri);
// Add services to the container.


builder.Services.AddControllersWithViews().AddJsonOptions(configure: ConfigureMVCJsonOptions);
builder.Services.AddAuthentication(configureOptions: ConfigureAuthentication).AddIdentityCookies();
builder.Services.AddControllersWithViews();
builder.Services.AddUserAccessor();
builder.Services.AddAccountsModule();
builder.Services.AddCQRSModules();
builder.Services.AddRedisTicketStore();
builder.Services.AddPersistenceModule(postgresUri);

builder.Services.AddExceptionHandler<ApplicationExceptionFilter>();

builder.Services.ConfigureApplicationCookie(static config => config.Cookie.Name = "_SSID");
builder.Services.Configure<AntiforgeryOptions>(static config => config.Cookie.Name = "_sameuser");


builder.Services.AddOptions<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme)
    .Configure<ITicketStore>(static (options, cache) => options.SessionStore = cache);


#region Configuration Options

void ConfigureMVCJsonOptions(JsonOptions options)
{
    options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
}
void ConfigureAuthentication(AuthenticationOptions options)
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}


#endregion
if (!builder.Environment.IsDevelopment())
{

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    var redis = builder.Configuration.GetRequiredSection("Redis");
    var redisClusters = redis.GetSection("Nodes").Get<List<string>>();
    var redisPassword = redis.GetSection("Password").Get<string>();
    ArgumentNullException.ThrowIfNull(redisClusters);
    ArgumentNullException.ThrowIfNull(redisPassword);
    builder.Services.AddRedisNodes(redisPassword, redisClusters);
}
else
{
    var redisUri = builder.Configuration.GetConnectionString("Redis");
    ArgumentNullException.ThrowIfNull(redisUri);
    builder.Services.AddRedis(redisUri);
    builder.Services.AddProblemDetails();

}

var app = builder.Build();

app.UseWhen(context =>
{
    var headers = context.Request.GetTypedHeaders();
    MediaTypeHeaderValue html = new(MediaTypeNames.Text.Html);
    return headers.Accept.Any(h => h.IsSubsetOf(html));
}, applicationBuilder =>
{
    if (builder.Environment.IsDevelopment())
        applicationBuilder.UseDeveloperExceptionPage();
    else
        applicationBuilder.UseExceptionHandler("/Home/Error");
}).UseWhen(context =>
{
    var headers = context.Request.GetTypedHeaders();
    MediaTypeHeaderValue json = new(MediaTypeNames.Application.Json);
    return headers.Accept.Any(h => h.IsSubsetOf(json));
}, applicationBuilder =>
{
    applicationBuilder.UseExceptionHandler();
});

// Configure the HTTP request pipeline.
// Configure Redis Service
if (!builder.Environment.IsDevelopment())
{

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

await app.CreateTestAsync(default);
await app.RunAsync();


