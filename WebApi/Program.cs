using Business.Concrete.Extensions;
using DataAccess.Concrete.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureDataAccess();
builder.Services.ConfigureServices();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
