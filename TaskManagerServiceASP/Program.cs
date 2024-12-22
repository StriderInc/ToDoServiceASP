using Microsoft.EntityFrameworkCore;
using TaskManagerServiceASP.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("TaskManagerDbContext");
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();
app.MapGet("/", (TaskManagerDbContext db) => db.Tasks.ToList());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();