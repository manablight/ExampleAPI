using ExampleAPI.Interfaces;
using ExampleAPI.Repositories;
using ExampleAPI.Services;
using ExampleAPI.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IMovieService, MovieService>();

builder.Services.AddValidatorsFromAssemblyContaining<MovieValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();