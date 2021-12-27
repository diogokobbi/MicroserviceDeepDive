using ShoppingCart.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddShoppingCartServices();

var app = builder.Build();

//app.MapGet("/", () => "System running!");

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
