using Microsoft.EntityFrameworkCore;
using SmartQueue.Api.Data;
using SmartQueue.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext (SQLite)
builder.Services.AddDbContext<QueueDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QueueDb")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITicketService, TicketService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// لاحقًا نقدر نرجع HTTPS
// app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
