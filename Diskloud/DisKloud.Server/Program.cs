using DisKloud.Server.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;



var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
//add filter


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

//app.UseAuthorization();

app.MapControllers();

Console.WriteLine("runing...");
app.Run();
