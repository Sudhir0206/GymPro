using GymPro.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymDb")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("GymProWeb", policy =>
        policy.WithOrigins("https://localhost:7281", "http://localhost:5281")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("GymProWeb");
app.MapControllers();

// Simple sample setup: create database/tables automatically.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GymDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.Run();
