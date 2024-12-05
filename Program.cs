using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Repository.implementation;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Implementation;
using RealEstate_Mvc_.Service.Interface;
using RealEstateMvc;
using System;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
{
    config.LoginPath = "/User/Login";
    config.LogoutPath = "/User/Logout";
    config.Cookie.Name = "RealEstateApp";
    config.AccessDeniedPath = "/User/Login";
    config.ExpireTimeSpan = TimeSpan.FromMinutes(240);
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartegoryRepository, CartegoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<ICartegoryService, CartegoryService>();
builder.Services.AddScoped <ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStaffService, StafffService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddDbContext<ContextClass>(config => config.UseSqlServer(builder.Configuration.GetConnectionString("PropertyDb")));







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
