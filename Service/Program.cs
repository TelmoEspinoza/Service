using Microsoft.EntityFrameworkCore;
using Service.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<MilkStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MilkStore") ?? throw new InvalidOperationException("Connection string 'MilkStoreContext' not found.")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowCredentials()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:4200"); // Replace with your React app's URL;
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.MapGet("/", () => "Hello world!");
app.MapControllers();
app.Run();


