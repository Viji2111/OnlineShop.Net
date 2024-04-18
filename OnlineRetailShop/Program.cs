
using BusinessLogicLayer.Implemetation;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PresentationLayer.Filters;
using Services.Interfaces;
using System;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.SaveToken = true;
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Issuer"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       };
   });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V1", new OpenApiInfo { Title = "OnlineRetailShop", Version = "V1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
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
        new String[]{}
        }

    });
});


builder.Services.AddDbContext<CommonContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineShopping")));

builder.Services.AddMemoryCache();
builder.Services.AddLazyCache();
builder.Services.AddScoped<AuthorizationFilter>();
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ICustomerService,CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/V1/swagger.json", "OnlineRetailShop V1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
