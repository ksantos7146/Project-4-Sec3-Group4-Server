using Microsoft.EntityFrameworkCore;
using Project_IV.Data;
using Project_IV.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GitCommitDbContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("mysql"));
});

builder.Services.AddScoped<Project_IV.Endpoints.UserEndpoint>(); // Register UserEndpoint with full namespace
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
