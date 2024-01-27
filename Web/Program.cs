using Microsoft.AspNetCore.Identity;
using Persistence;
using Application;
using Persistence.Data;
using Application.Common;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddIdentity<Domain.Entities.User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<CookbookDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(op =>
{
    op.Password.RequireDigit = false;
    op.Password.RequireUppercase = false;
    op.Password.RequireLowercase = false;
    op.Password.RequireNonAlphanumeric = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/user/signin";
    options.AccessDeniedPath = "/user/signout";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapRazorPages();

app.Run();
