using System.Text.Json.Serialization;
using Google.Cloud.Firestore;
using Hangfire;
using Hangfire.PostgreSql;
using LittleConqueror;
using LittleConqueror.API.Mappers;
using LittleConqueror.AppService.Domain.Handlers.ActionHandlers;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;
using LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.Domain.Strategies;
using LittleConqueror.AppService.Domain.Strategies.ActionStrategies;
using LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Remove;
using LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Set;
using LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Authentication;
using LittleConqueror.Exceptions;
using LittleConqueror.Infrastructure;
using LittleConqueror.Infrastructure.DatabaseAdapters;
using LittleConqueror.Infrastructure.FetchingAdapters;
using LittleConqueror.Infrastructure.JwtAdapters;
using LittleConqueror.Infrastructure.Repositories;
using LittleConqueror.Infrastructure.Repositories.Firebases;
using LittleConqueror.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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
    
    // polymorphism
    c.UseOneOfForPolymorphism();
    c.UseAllOfForInheritance();
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

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AppExceptionFiltersAttribute>();
}).AddNewtonsoftJson(options =>
    {
        // Configurations supplémentaires si nécessaire
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        
        // Pour les enums
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        
        // dictionary keys to camelCase 
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
    });

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "littleconquerorconfigs-firebase-adminsdk-t0asg-cadf5ee4f2.json");

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

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(c =>
        c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangfireConnection"))));

builder.Services.AddHangfireServer();

// Services Driving
builder.Services.AddScoped<ICreateUserHandler, CreateUserHandler>()
    .AddScoped<IGetTerritoryByUserIdHandler, GetTerritoryByUserIdHandler>()
    .AddScoped<IGetTerritoryCitiesWithGeoJsonHandler, GetTerritoryCitiesWithGeoJsonByUserIdHandler>()
    .AddScoped<IGetUserByIdHandler, GetUserByIdHandler>()
    .AddScoped<IGetCityByLongitudeAndLatitudeHandler, GetCityByLongitudeAndLatitudeHandler>()
    .AddScoped<IGetUserInformationsHandler, GetUserInformationsHandler>()
    .AddScoped<IConsumeRegistrationLinkRelatedDataHandler, ConsumeRegistrationLinkRelatedDataHandler>()
    .AddScoped<IConsumeForgotPasswordLinkRelatedDataHandler, ConsumeForgotPasswordLinkRelatedDataHandler>()
    .AddScoped<ICreateRegistrationLinkHandler, CreateRegistrationLinkHandler>()
    .AddScoped<ICreateForgetPasswordLinkHandler, CreateForgetPasswordLinkHandler>()
    .AddScoped<IChangePasswordHandler, ChangePasswordHandler>()
    .AddScoped<IRegisterAuthUserHandler, RegisterAuthUserHandler>()
    .AddScoped<IChangePasswordHandler, ChangePasswordHandler>()
    .AddScoped<IGetAuthenticatedUserByIdHandler, GetAuthenticatedUserByIdHandler>()
    .AddScoped<IAuthenticateUserHandler, AuthenticateUserHandler>()
    .AddScoped<IAddCityToATerritoryHandler, AddCityToATerritoryHandler>()
    .AddScoped<IGetCityByOsmIdHandler, GetCityByOsmIdHandler>()
    .AddScoped<ICreateTerritoryHandler, CreateTerritoryHandler>()
    .AddScoped<ICreateResourcesForUserHandler, CreateResourcesForUserHandler>()
    .AddScoped<IGetResourcesForUserHandler, GetResourcesForUserHandler>()
    .AddScoped<IStrategyContext, StrategyContext>()
    .AddScoped<ISetActionToCityHandler, SetActionToCityHandler>()
    .AddScoped<IRemoveActionOfCityHandler, RemoveActionOfCityHandler>()
    .AddScoped<IGetPaginatedActionsByUserIdHandler, GetPaginatedActionsByUserIdHandler>()
    .AddScoped<IGetResourceDetailsHandler, GetResourceDetailsHandler>()
    .AddScoped<IGetTechTreeOfUserIdHandler, GetTechTreeOfUserIdHandler>()
    .AddScoped<IGetSciencePointsOfUserIdHandler, GetSciencePointsOfUserIdHandler>()
    .AddScoped<ISetTechToResearchOfUserIdHandler, SetTechToResearchOfUserIdHandler>()
    .AddScoped<ICancelTechResearchOfUserIdHandler, CancelTechResearchOfUserIdHandler>()
    .AddScoped<ICompleteTechResearchOfUserIdHandler, CompleteTechResearchOfUserIdHandler>()

// Strategies KeyedServices
    .AddKeyedScoped<ISetActionStrategy, SetActionAgricoleStrategy>(ActionType.Agricole)
    .AddKeyedScoped<ISetActionStrategy, SetActionMiniereStrategy>(ActionType.Miniere)
    .AddKeyedScoped<ISetActionStrategy, SetActionTechnologiqueStrategy>(ActionType.Technologique)
    .AddKeyedScoped<IRemoveActionStrategy, RemoveActionAgricoleStrategy>(ActionType.Agricole)
    .AddKeyedScoped<IRemoveActionStrategy, RemoveActionMiniereStrategy>(ActionType.Miniere)
    .AddKeyedScoped<IRemoveActionStrategy, RemoveActionTechnologiqueStrategy>(ActionType.Technologique)
    
    .AddKeyedScoped<IGetResourceDetailsStrategy, GetFoodResourceDetailsStrategy>(ResourceType.Food)
    .AddKeyedScoped<IGetResourceDetailsStrategy, GetWoodResourceDetailsStrategy>(ResourceType.Wood)
    .AddKeyedScoped<IGetResourceDetailsStrategy, GetStoneResourceDetailsStrategy>(ResourceType.Stone)
    .AddKeyedScoped<IGetResourceDetailsStrategy, GetIronResourceDetailsStrategy>(ResourceType.Iron)
    .AddKeyedScoped<IGetResourceDetailsStrategy, GetGoldResourceDetailsStrategy>(ResourceType.Gold)
    .AddKeyedScoped<IGetResourceDetailsStrategy, GetDiamondResourceDetailsStrategy>(ResourceType.Diamond)
    .AddKeyedScoped<IGetResourceDetailsStrategy, GetPetrolResourceDetailsStrategy>(ResourceType.Petrol)
    
    
// Services Driven
    .AddScoped<IOSMCityFetcherPort, NominatimOSMFetcherAdapter>()
    .AddScoped<ICityDatabasePort, CityDatabaseAdapter>()
    .AddScoped<IUserDatabasePort, UserDatabaseAdapter>()
    .AddScoped<ITerritoryDatabasePort, TerritoryDatabaseAdapter>()
    .AddScoped<IJwtTokenProviderPort, JwtTokenProviderAdapter>()
    .AddScoped<IPasswordHasherPort, PasswordHasherAdapter>()
    .AddScoped<IAuthUserDatabasePort, AuthUserDatabaseAdapter>()
    .AddScoped<ITransactionManagerPort, TransactionManagerAdapter>()
    .AddScoped<ITransactionManagerPort, TransactionManagerAdapter>()
    .AddScoped<IResourcesDatabasePort, ResourcesDatabaseAdapter>()
    .AddScoped<IActionDatabasePort, ActionDatabaseAdapter>()
    .AddScoped<ITechResearchDatabasePort, TechResearchDatabaseAdapter>()
    .AddScoped<UserRepository>()
    .AddScoped<TerritoryRepository>()
    .AddScoped<CityRepository>()
    .AddScoped<AuthUserRepository>()
    .AddScoped<ResourcesRepository>()
    .AddScoped<ActionRepository>()
    .AddScoped<TechResearchRepository>()
    .AddScoped<TechConfigsRepository>(repo => 
        new TechConfigsRepository(FirestoreDb.Create("littleconquerorconfigs")))

// Others
    .AddAutoMapper(typeof(MappingProfile))
    .ConfigureJwt(builder.Configuration.GetSection("AppSettings").Get<AppSettings>())
    .Configure<OSMSettings>(builder.Configuration.GetSection("OSMSettings"))
    .AddScoped<IUserContext, UserContext>()
    .AddSingleton<ITemporaryCodeService, TemporaryCodeService>()
    .AddSingleton<ITokenManagerService, TokenManagerService>()
    .AddScoped<ITechRulesServices, TechRulesServices>()
    .AddSingleton<IBackgroundJobService, BackgroundJobService>()

// HttpClients
    .AddHttpClient("NominatimOSM", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("LittleConqueror/1.0");
    httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("fr-FR,fr;q=0.9,en-US;q=0.8,en;q=0.7");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserContextMiddleware>();
app.UseMiddleware<TokenBlacklistMiddleware>();

app.UseHttpsRedirection();
app.MapControllers().WithOpenApi();

app.Run();