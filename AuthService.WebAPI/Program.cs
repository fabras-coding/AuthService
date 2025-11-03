using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Infra.IoC;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


// Minimal API endpoint
app.MapPost("/token", async (LoginDTO loginDto, ILoginAppService loginService) =>
{
    var access_token = await loginService.AuthenticateAsync(loginDto);
    return Results.Ok(new { access_token });
});

app.MapGet("/validate", (ClaimsPrincipal user) =>
{
    if (user.Identity is not null && user.Identity.IsAuthenticated)
        return Results.Ok(new { message = "Token is valid." });
    return Results.Unauthorized();
})
.RequireAuthorization(); ;


app.MapGet("/random-wheather", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var rng = new Random();
    var weatherForecasts = Enumerable.Range(1, 5).Select(index => new
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = summaries[rng.Next(summaries.Length)]
    })
    .ToArray();

    return Results.Ok(weatherForecasts);
}).RequireAuthorization();

app.Run();
