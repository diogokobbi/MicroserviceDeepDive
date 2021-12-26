var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Scan(selector => selector.FromAssemblyOf<Program>().AddClasses().AsImplementedInterfaces());

var app = builder.Build();

app.MapGet("/", () => "System running!");

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
