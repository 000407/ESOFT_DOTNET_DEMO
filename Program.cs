using demo_crud.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

ConfigurationManager configurationManager = builder.Configuration;

builder.Services.AddDbContext<StoreContext>(options => { // Setting up DbContext in the application container
    var config = builder.Configuration;
    options.UseMySQL("server=localhost;user=root;database=demo_crud;port=3306;password=root");
});

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

app.UseMvc(
    // routes => // We DON'T need this anymore. We will define routes in the controllers itself using Attributes
    // // https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1#attribute-routing-for-rest-apis-1
    //    {
    //         routes.MapRoute(
    //             name: "getAllProducts",
    //             template: "{controller=Product}/{action=Index}");
    //    }
);

app.Run();
