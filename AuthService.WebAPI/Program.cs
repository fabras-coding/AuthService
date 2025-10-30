using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();


// Minimal API endpoint
app.MapPost("/token", async (LoginDTO loginDto, ILoginAppService loginService) =>
{
    
    var token = await loginService.AuthenticateAsync(loginDto);
    return Results.Ok(new { token });
});

app.Run();
