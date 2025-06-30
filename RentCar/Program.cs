using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using SendEmail.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar conexión a base de datos SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexionSQL"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

//  Configurar Identity con roles y EF Core
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Configurar cookie de autenticación explícitamente
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);       
    options.SlidingExpiration = true;                      
    options.LoginPath = "/Identity/Account/Login";         
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; 
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always; 
});

// Servicios MVC y otros
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<EmailService>();
builder.Services.AddSingleton<WhatsAppService>();

var app = builder.Build();

//  Aplicar migraciones y seed roles/usuario
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRolesAndSuperUser(userManager, roleManager);
}

//  Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint();
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

//  Método para seed roles y superusuario
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
            Console.WriteLine("SuperUsuario creado y rol asignado.");
        }
        else
        {
            Console.WriteLine("Error creando SuperUsuario:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($" - {error.Description}");
            }
        }
    }
    else
    {
        Console.WriteLine("SuperUsuario ya existe.");
    }
}
