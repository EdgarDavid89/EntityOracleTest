using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Maper;
using DataAccess.Business;
using DataAccess;
using Parkings;

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
builder.Services.AddScoped<IWorkerB<decimal>, WorkerB<decimal>>();
builder.Services.AddScoped<IRepository<Worker, decimal>, Repository<Worker, decimal>>();
builder.Services.AddScoped<IParkingB<decimal>, ParkingB<decimal>>();
builder.Services.AddScoped<IRepository<Parking, decimal>, Repository<Parking, decimal>>();

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
