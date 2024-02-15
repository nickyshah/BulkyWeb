
using Bulky.DataAccess.Data;
using Bulky.DataAccess.DbInitializer;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Utility;
using BulkyBook.DataAccess.DbInitializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));



builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(
    options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(100);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddAuthentication().AddFacebook(opt =>
{
    opt.ClientId = "1333902063956679";
    opt.ClientSecret = "207c0fb9651056496f634dcb993f3e0e";
});

builder.Services.AddAuthentication().AddMicrosoftAccount(opt =>
{
    opt.ClientId = "6KS8Q~VWXz10UEUiRrI9Xin2X5F-~jmY2gdl1csP";
    opt.ClientSecret = "2de2a47a-e3c1-4f4f-b054-b475d65757ac";
});

builder.Services.AddAuthentication().AddGoogle(opt =>
{
    opt.ClientId = "233043904565-cde6ifrg1m6acpoqopln9ib2690u04ab.apps.googleusercontent.com";
    opt.ClientSecret = "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";
});

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
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

SeedDatabase();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
