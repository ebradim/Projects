using ExecutionStrategy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(builder =>
{
    builder.UseNpgsql(conf =>
    {
        conf.EnableRetryOnFailure(5);
        conf.ExecutionStrategy(deps => new CustomExecutionStrategy(deps));
    });

});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.Run();
