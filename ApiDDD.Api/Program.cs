using ApiDDD.Data.Repositories;
using ApiDDD.Data.Configuration;
using SalesAPI.Infrastructure.Services;
using SalesAPI.Application.Services;
using SalesAPI.Domain.Repositories;
using SalesAPI.Domain.Events;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB
MongoDbConfiguration.Configure();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ApiDDD.Api", Version = "v1" });
});

// Register services
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IEventLogger, EventLogger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiDDD.Api v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();