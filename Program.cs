using StudentManagementSystem.Model;
using StudentManagementSystem.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentManagementSystem.Process;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static StudentManagementSystem.Providers.AccessProviders;
using StudentManagementSystem.Controllers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using StudentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<User>();
builder.Services.AddScoped<CalculateGrad>();
object value = builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddAuthentication(a => { a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; }).AddJwtBearer(a => { a.RequireHttpsMetadata = false; a.TokenValidationParameters = new() { IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigProvider.EncryptionKey)), ValidateIssuer = false, ValidateLifetime = true, ClockSkew = TimeSpan.Zero, ValidateAudience = false }; });
builder.Services.AddAuthorization(options => { options.AddPolicy("Teacher", policy => policy.RequireRole("Teacher")); });
builder.Services.AddSwaggerGen(s => { s.AddSecurityDefinition("Bearer", new() { Name = "Authorization", In = ParameterLocation.Header, Type = SecuritySchemeType.ApiKey, Description = "Enter JWT with Bearer into field" }); s.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } }); });
var app = builder.Build();
//builder.Services.AddScoped<LoginProcess>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();