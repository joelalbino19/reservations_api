using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Reservation.Application.Interfaces;
using Reservation.Application.Interfaces.Auth;
using Reservation.Application.Interfaces.Reservation;
using Reservation.Application.Interfaces.Space;
using Reservation.Application.Interfaces.User;
using Reservation.Application.Services.Auth;
using Reservation.Application.Services.Reservation;
using Reservation.Application.Services.Space;
using Reservation.Application.Services.User;
using Reservation.Application.Services.Util;
using Reservation.Domain.Interfaces;
using Reservation.Infrastructure.Context;
using Reservation.Infrastructure.Repository.Reservation;
using Reservation.Infrastructure.Repository.Space;
using Reservation.Infrastructure.Repository.User;
using ReservationsAPI.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


string? connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
string key = builder.Configuration["jwt:Key"] ?? "";
string issuer = builder.Configuration["jwt:Issuer"] ?? "";
string audience = builder.Configuration["jwt:Audience"] ?? "";

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
                                             builder => builder.AllowAnyOrigin()
                                                               .AllowAnyHeader()
                                                               .AllowAnyMethod()));

builder.Services.AddDbContext<ReservationDbContext>(options => options.UseSqlServer(connectionString));

#region Repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();

#endregion

#region Services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IUtilService, UtilService>();
builder.Services.AddScoped<ISpaceService, SpaceService>();

#endregion

#region AddAuthentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });
#endregion

#region AddSwaggerGen
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
              Array.Empty<string>()
        }
    });
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();

app.UseCors("AllowWebApp");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
