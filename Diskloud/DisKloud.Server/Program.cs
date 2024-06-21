using DisKloud.Server.Contexts;
using Microsoft.EntityFrameworkCore;




System.IO.Directory.CreateDirectory(".\\Files");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//bd Context
builder.Services.AddDbContext<AppDbContext>();

//use swagger
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//use swagger




app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
