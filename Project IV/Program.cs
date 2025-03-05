using Microsoft.EntityFrameworkCore;
using Project_IV.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GitCommitDbContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("mysql"));
});


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
