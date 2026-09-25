using Microsoft.EntityFrameworkCore;
using PV521_BooksShop.DAL;
using PV521_BooksShop.DAL.Repositories;
using PV521_BookssShop.Services;
using Scalar.AspNetCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using PV521_BookssShop.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ImageService>();

builder.Services.AddScoped<GenreRepostiory>();
builder.Services.AddScoped<GenreService>();

builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<AuthorRepostiory>();
builder.Services.AddScoped<AuthorService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorValidator>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("localDb");

    options.UseNpgsql(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();