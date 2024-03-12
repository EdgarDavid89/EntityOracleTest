using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Maper;
using DataAccess.Business;
using DataAccess;
using Parkings;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Parkings.Secutiry;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Parkings API oracle11g EF Concept", Version = "v1" });
    c.OperationFilter<FiltersSwagger>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});

builder.Services.AddAutoMapper(typeof(MappingProfileDB));
builder.Services.AddScoped<IWorkerB, WorkerB>();
builder.Services.AddScoped<IRepository<Worker>, Repository<Worker>>();
builder.Services.AddScoped<IParkingB, ParkingB>();
builder.Services.AddScoped<IRepository<Parking>, Repository<Parking>>();
builder.Services.AddScoped<IRepository<Resource>, Repository<Resource>>();
builder.Services.AddScoped<IResourceB, ResourceB>();
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IUserB, UserB>();
builder.Services.AddScoped<IAuthentication, Authentication>();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelContext>(x => x.UseOracle(connectionString,
options => options.UseOracleSQLCompatibility("11"))
);

string key = builder.Configuration.GetValue<string>("JwtSettings:Key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parkings API oracle11g EF Concept v1");
        c.RoutePrefix = string.Empty;
        c.OAuthClientId("swagger");
        c.OAuthAppName("Swagger UI");
    });

}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
