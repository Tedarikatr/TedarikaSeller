using TedarikaSeller.Services;
using TedarikaSeller.ServicesAbstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session service
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add in-memory cache for session storage
builder.Services.AddDistributedMemoryCache();

// Add HttpClient for AuthService
//builder.Services.AddScoped<IAuthService, AuthService>();

//builder.Services.AddScoped<ICompanyService, CompanyService>();
//builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddHttpClient(); // T�m HTTP istemci istekleri i�in genel ekleme

// AuthService ve di�er HTTP kullanan servisler i�in HttpClient kullan�m� ekleniyor
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<ICompanyService, CompanyService>();
builder.Services.AddHttpClient<IShopService, ShopService>();


var app = builder.Build();

// Middleware configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session middleware'i burada aktif etmelisiniz
app.UseSession();

// Authentication ve Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
