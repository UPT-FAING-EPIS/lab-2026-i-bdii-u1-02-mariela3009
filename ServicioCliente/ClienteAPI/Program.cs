using ClienteAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BdClientesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClienteDB")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/swagger", () => Results.Redirect("/openapi/v1.json"));
app.MapControllers();

app.Run();
