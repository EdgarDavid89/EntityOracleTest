using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Maper;
using DataAccess.Business;
using DataAccess;
using Parkings;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{
    c.SwaggerDoc("v1", new() { Title = "Parkings API oracle11g EF Concept", Version = "v1" });
    c.OperationFilter<FiltersSwagger>();

});
builder.Services.AddAutoMapper(typeof(MappingProfileDB));
builder.Services.AddScoped<IWorkerB, WorkerB>();
builder.Services.AddScoped<IRepository<Worker>, Repository<Worker>>();
builder.Services.AddScoped<IParkingB, ParkingB>();
builder.Services.AddScoped<IRepository<Parking>, Repository<Parking>>();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelContext>(x => x.UseOracle(connectionString, 
options => options.UseOracleSQLCompatibility("11"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
