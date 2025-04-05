using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var sqlConnectionString = builder.Configuration.GetValue<string>("SqlConnectionString");
//var sqlConnectionString = builder.Configuration["SqlConnectionString"];
builder.Services.AddScoped<DoctorRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<DoctorRepository>>(); // Get logger from DI container
    return new DoctorRepository(sqlConnectionString, logger); // Inject the logger into the repository
}); 
builder.Services.AddScoped<PatientRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<PatientRepository>>(); // Get logger from DI container
    return new PatientRepository(sqlConnectionString, logger); // Inject the logger into the repository
});
builder.Services.AddScoped<TreatmentPlanRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<TreatmentPlanRepository>>(); // Get logger from DI container
    return new TreatmentPlanRepository(sqlConnectionString, logger); // Inject the logger into the repository
});
builder.Services.AddScoped<TreatmentPlanCareMomentRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<TreatmentPlanCareMomentRepository>>(); // Get logger from DI container
    return new TreatmentPlanCareMomentRepository(sqlConnectionString, logger); // Inject the logger into the repository
});
builder.Services.AddScoped(_ => new GuardianRepository(sqlConnectionString));
builder.Services.AddScoped(_ => new CareMomentRepository(sqlConnectionString));

builder.Services.AddScoped<UserRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<UserRepository>>(); // Get logger from DI container
    return new UserRepository(sqlConnectionString, logger); // Inject the logger into the repository
});

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();  // Log to the console
    logging.AddDebug();    // Log to the debug output
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "AdminScheme";
    options.DefaultAuthenticateScheme = "AdminScheme";
    options.DefaultChallengeScheme = "AdminScheme";
})
.AddCookie("AdminScheme", options =>
{
    options.LoginPath = "/Admin/Login";
    options.AccessDeniedPath = "/Admin/Denied";
    options.Cookie.Name = "AdminAuth";
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/"); 
    // Optional: allow anonymous access to the login page
    options.Conventions.AllowAnonymousToPage("/Admin/Login");
});


builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddDapperStores(options =>
    {
        options.ConnectionString = sqlConnectionString;
    });

var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);

var app = builder.Build();

//app.MapGet("/", () => $"The API is up yes. Connection string found: {(sqlConnectionStringFound ? "yes" : "no")}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/account")
.MapIdentityApi<IdentityUser>();

app.MapControllers()
    .RequireAuthorization();
app.MapRazorPages();

app.Run();
