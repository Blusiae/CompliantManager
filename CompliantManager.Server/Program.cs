using CompliantManager.Server.Data;
using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Services.Implementations;
using CompliantManager.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=.;Database=CompliantManagerDB;Trusted_Connection=True;Encrypt=False;"));
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

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<InitialSetupService>();

builder.Services.AddAuthorization();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.Migrate();

var setupService = scope.ServiceProvider.GetRequiredService<InitialSetupService>();
if (!setupService.IsInitialized())
{
    var email = "admin@example.com";
    var password = "Admin123!";
    var firstName = "Admin";
    var lastName = "User";

    await setupService.InitializeAsync(email, password, firstName, lastName);
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

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
