using demo_crud.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

ConfigurationManager configurationManager = builder.Configuration;

builder.Services.AddSingleton(typeof(ProductDataAccessContext), new ProductDataAccessContext("server=localhost;user=root;database=demo_crud;port=3306;password=root"));
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "default",
                   template: "{controller=Product}/{action=Index}");
           });

app.Run();
