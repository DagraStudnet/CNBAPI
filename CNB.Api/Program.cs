using System.Text.Json;
using System.Text.Json.Serialization;
using CNB.Api.Clients;
using CNB.Api.Configuration;
using CNB.Api.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers((options =>
    {
        options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
        options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        }));
    }))
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration
var configuration = builder.Configuration;
builder.Services.AddSingleton(configuration.GetSection("CnbApi").Get<CNBApiConfiguration>());

// Custom Services
builder.Services.AddScoped<YearExchangeRateServiceService>();
builder.Services.AddScoped<DayExchangeRateServiceService>();
builder.Services.AddScoped<CNBExchangeRateClient>();



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
