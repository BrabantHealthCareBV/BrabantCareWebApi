using BrabantCareWebApi;
using Microsoft.AspNetCore.Identity;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using BrabantCareWebApi.Repositories;
using ProjectMap.WebApi.Repositories;
using Microsoft.DotNet.Scaffolding.Shared;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddRazorPages();  // Razor Pages
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var sqlConnectionString = builder.Configuration.GetValue<string>("SqlConnectionString");
//var sqlConnectionString = builder.Configuration["SqlConnectionString"];
builder.Services.AddScoped<DoctorRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<DoctorRepository>>(); // Get logger from DI container
    return new DoctorRepository(sqlConnectionString, logger); // Inject the logger into the repository
});
builder.Services.AddScoped(_ => new PatientRepository(sqlConnectionString));
builder.Services.AddScoped(_ => new GuardianRepository(sqlConnectionString)); 
builder.Services.AddScoped<TreatmentPlanRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<TreatmentPlanRepository>>(); // Get logger from DI container
    return new TreatmentPlanRepository(sqlConnectionString, logger); // Inject the logger into the repository
});
builder.Services.AddScoped(_ => new CareMomentRepository(sqlConnectionString));

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();  // Log to the console
    logging.AddDebug();    // Log to the debug output
});


builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddDapperStores(options =>
    {
        options.ConnectionString = sqlConnectionString;
    });

var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);

var app = builder.Build();

app.MapGet("/", () => $"The API is up ??. Connection string found: {(sqlConnectionStringFound ? "?" : "?")}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGroup("/account")
.MapIdentityApi<IdentityUser>();

app.MapControllers()
    .RequireAuthorization();
app.MapRazorPages();

app.Run();
