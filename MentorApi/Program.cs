using MentorApi.Abstractions.IUnitOfWorks;
using MentorApi.Abstractions.Services;
using MentorApi.Context;
using MentorApi.Extensions;
using MentorApi.Implementations;
using MentorApi.Implementations.UnitOfWorks;
using MentorApi.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using FluentValidation.AspNetCore;
using MentorApi.DTOs.SchoolDTOs;
using MentorApi.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(confiq=>confiq.RegisterValidatorsFromAssemblyContaining<SchoolCreateDTOValidator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configure Entity Framework and Identity
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser,AppRole>()
    .AddEntityFrameworkStores<SchoolDbContext>()
    .AddDefaultTokenProviders();

// Register Unit of Work and Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Register services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService,  RoleService>();
builder.Services.AddScoped<ITokenHandler, MentorApi.Implementations.TokenHandler>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = true,//tokunumuzu kim/hansi origin islede biler
        ValidateIssuer = true, //tokunu kim palylayir
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true, //tokenin ozel keyi

        ValidAudience = builder.Configuration["Token:Audience"],

        ValidIssuer = builder.Configuration["Token:Issuer"],

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),

        //token omru qeder islemesi ucun
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };


});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandler();
app.UseAuthorization();

app.MapControllers();

app.Run();
