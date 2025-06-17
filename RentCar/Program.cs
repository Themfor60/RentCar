using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Data.Data.Repository.IRepository;
using SendEmail.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()  
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<EmailService>();


builder.Services.AddSingleton<WhatsAppService>();



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRolesAndSuperUser(userManager, roleManager);
}


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();



async Task SeedRolesAndSuperUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    
    string[] roles = new[] { "SuperUsuario", "Admin", "Usuario" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

   
    var superUserEmail = "superusuario@tudominio.com";
    var superUser = await userManager.FindByEmailAsync(superUserEmail);

    if (superUser == null)
    {
        var newSuperUser = new IdentityUser
        {
            UserName = superUserEmail,
            Email = superUserEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newSuperUser, "TuPasswordSeguro123!");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newSuperUser, "SuperUsuario");
        }
    }
}
