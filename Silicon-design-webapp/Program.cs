var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


//add services here such as repositories, services, dbcontext etc.


var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
