using CompliantManager.Server.Data;
using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Implementations;
using CompliantManager.Server.Repositories.Interfaces;
using CompliantManager.Server.Services.Implementations;
using CompliantManager.Server.Services.Interfaces;
using CompliantManager.Shared.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddSqlServerDbContext<ApplicationDbContext>("Database");
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
// Register MailingService with connection string from configuration
builder.Services.AddScoped<IMailingService>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("AzureCommunicationServices");
    return new MailingService(connectionString);
});

builder.Services.AddScoped<InitialSetupService>();

builder.Services.AddAuthorization();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

var retries = 0;
while (true)
{
    try
    {
        dbContext.Database.Migrate();
        break;
    }
    catch (Exception ex) when (retries < 10)
    {
        retries++;
        app.Logger.LogWarning(ex, "Migration failed (attempt {Attempt}/10), retrying in {Delay}s...", retries, retries * 2);
        await Task.Delay(TimeSpan.FromSeconds(retries * 2));
    }
}

var setupService = scope.ServiceProvider.GetRequiredService<InitialSetupService>();
if (!setupService.IsInitialized())
{
    UserDto user = new();
    app.Configuration.GetSection("AdminUser").Bind(user);

    await setupService.InitializeAsync(user);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
