using Duende.Bff;
using Duende.Bff.Yarp;
using UserService.Bff;
using UserService.Bff.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("yarp.instruments.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddReverseProxy()
    .AddBffExtensions()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddBff()
    .AddRemoteApis();
builder.Services.AddTransient<IReturnUrlValidator, FrontendHostReturnUrlValidator>();

var config = new Configuration();
builder.Configuration.Bind("BFF", config);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "__Host-bff";
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = config.Authority;
        options.ClientId = config.ClientId;
        options.ClientSecret = config.ClientSecret;

        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.GetClaimsFromUserInfoEndpoint = true;
        options.MapInboundClaims = false;
        options.DisableTelemetry = true;

        options.SaveTokens = true;

        options.Scope.Clear();
        foreach (var scope in config.Scopes)
        {
            options.Scope.Add(scope);
        }
        
        options.TokenValidationParameters.RoleClaimType = "role";
        options.TokenValidationParameters.NameClaimType = "name";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseBff();

app.UseAuthorization();

app.MapReverseProxy()
    .AsBffApiEndpoint();

app.MapControllers();

app.MapBffManagementEndpoints();

app.Run();