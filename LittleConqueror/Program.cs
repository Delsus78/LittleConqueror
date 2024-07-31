using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.DrivingPorts;
using LittleConqueror.Exceptions;
using LittleConqueror.Infrastructure.FetchingAdapters;
using LittleConqueror.Models.Mappers;
using LittleConqueror.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddDbContext<DataContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Services Driving
builder.Services.AddScoped<ICityService, CityService>();

// Services Driven
builder.Services.AddScoped<IOSMCityFetcher, NominatimOSMFetcherAdapter>();

// Others
builder.Services.AddAutoMapper(typeof(MappingProfile));


// HttpClients
builder.Services.AddHttpClient("NominatimOSM", httpClient =>
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

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers()
    .WithOpenApi();

app.Run();