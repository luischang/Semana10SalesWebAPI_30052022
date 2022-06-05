using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Semana10Sales.DOMAIN.Core.Interfaces;
using Semana10Sales.DOMAIN.Infrastructure.Data;
using Semana10Sales.DOMAIN.Infrastructure.Mapping;
using Semana10Sales.DOMAIN.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Get DevConnection
var cnx = builder.Configuration.GetConnectionString("DevConnection");
//Add DbContext
builder.Services.AddDbContext<SalesContext>(options => options.UseSqlServer(cnx));

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperProfile());


});

var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
