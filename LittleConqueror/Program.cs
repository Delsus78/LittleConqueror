using LittleConqueror;
using LittleConqueror.API.Mappers;
using LittleConqueror.AppService.Domain.Handlers;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Domain.Singletons;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Authentication;
using LittleConqueror.Exceptions;
using LittleConqueror.Infrastructure;
using LittleConqueror.Infrastructure.DatabaseAdapters;
using LittleConqueror.Infrastructure.FetchingAdapters;
using LittleConqueror.Infrastructure.JwtAdapters;
using LittleConqueror.Infrastructure.Repositories;
using LittleConqueror.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Little Conqueror", Version = "v1" });
    c.CustomSchemaIds(x => x.FullName); // Enables to support different classes with the same name using the full name with namespace
    c.SchemaFilter<NamespaceSchemaFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        Console.Out.WriteLine("Adding cors policy");
        corsPolicyBuilder.WithOrigins("http://localhost:5173", "https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Important pour SignalR
    });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AppExceptionFiltersAttribute>();
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<DataContext>(options => 
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")))
    .Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"))
    .AddOptions<AppSettings>()
        .Bind(builder.Configuration.GetSection("AppSettings"))
        .ValidateDataAnnotations()
        .Validate(appSettings =>
        {
            if (string.IsNullOrEmpty(appSettings.Secret))
                throw new ArgumentException("Secret is missing");
            if (string.IsNullOrEmpty(appSettings.Issuer))
                throw new ArgumentException("Issuer is missing");
            if (string.IsNullOrEmpty(appSettings.Audience))
                throw new ArgumentException("Audience is missing");
            if (appSettings.ExpirationInMinutes <= 0)
                throw new ArgumentException("ExpirationInMinutes must be greater than 0");

            return true;
        });

// Services Driving
builder.Services.AddScoped<ICreateUserHandler, CreateUserHandler>()
    .AddScoped<IGetTerritoryByUserIdHandler, GetTerritoryByUserIdHandler>()
    .AddScoped<IGetUserByIdHandler, GetUserByIdHandler>()
    .AddScoped<IGetCityByLongitudeAndLatitudeHandler, GetCityByLongitudeAndLatitudeHandler>()
    .AddScoped<IGetUserInformationsHandler, GetUserInformationsHandler>()
    .AddScoped<IGetRegistrationLinkRelatedDataHandler, GetRegistrationLinkRelatedDataHandler>()
    .AddScoped<ICreateRegistrationLinkHandler, CreateRegistrationLinkHandler>()
    .AddScoped<IRegisterAuthUserHandler, RegisterAuthUserHandler>()
    .AddScoped<IGetAuthenticatedUserByIdHandler, GetAuthenticatedUserByIdHandler>()
    .AddScoped<IAuthenticateUserHandler, AuthenticateUserHandler>()
    .AddScoped<IAddCityToATerritoryHandler, AddCityToATerritoryHandler>()
    .AddScoped<IGetCityByOsmIdHandler, GetCityByOsmIdHandler>()

// Services Driven
    .AddScoped<IOSMCityFetcherPort, NominatimOSMFetcherAdapter>()
    .AddScoped<ICityDatabasePort, CityDatabaseAdapter>()
    .AddScoped<IUserDatabasePort, UserDatabaseAdapter>()
    .AddScoped<ITerritoryDatabasePort, TerritoryDatabaseAdapter>()
    .AddScoped<IJwtTokenProviderPort, JwtTokenProviderAdapter>()
    .AddScoped<IPasswordHasherPort, PasswordHasherAdapter>()
    .AddScoped<IAuthUserDatabasePort, AuthUserDatabaseAdapter>()
    .AddScoped<UserRepository>()
    .AddScoped<TerritoryRepository>()
    .AddScoped<CityRepository>()
    .AddScoped<AuthUserRepository>()

// Others
    .AddAutoMapper(typeof(MappingProfile))
    .ConfigureJwt(builder.Configuration.GetSection("AppSettings").Get<AppSettings>())
    .AddSingleton<IRegistrationLinkService, RegistrationLinkService>()
    .AddSingleton<ITokenManagerService, TokenManagerService>()
// HttpClients
    .AddHttpClient("NominatimOSM", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("LittleConqueror/1.0");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TokenBlacklistMiddleware>();


app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseHttpsRedirection();
app.MapControllers().WithOpenApi();

app.Run();