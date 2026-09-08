using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL;
using PV521_BooksShop.DAL.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("localDb");

    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<AuthorRepostiory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();