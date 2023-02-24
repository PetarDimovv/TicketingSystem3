using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketingSystem3.Data.Data;
using TicketingSystem3.Data.Data.CustomRoles;
using TicketingSystem3.Data.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<ICustomRoleManager, CustomRoleManager>();



var app = builder.Build();

//using var scope = app.Services.CreateScope();
//var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//if (!await roleManager.RoleExistsAsync("Admin"))
//{
//    await roleManager.CreateAsync(new IdentityRole("Admin"));
//}
//if (!await roleManager.RoleExistsAsync("Support"))
//{
//    await roleManager.CreateAsync(new IdentityRole("Support"));
//}
//if (!await roleManager.RoleExistsAsync("Customer"))
//{
//    await roleManager.CreateAsync(new IdentityRole("Customer"));
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapRazorPages();



app.Run();
