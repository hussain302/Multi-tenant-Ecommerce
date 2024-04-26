using Application;
using Infrastructure;
using API.Middlewares;
using System.Text.Json;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using API.Infrastructure.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region API, Application and Infrastructure Servcies
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
#endregion

#region Swagger Servies
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "E-Commerce Store Api",
        Version = "Internal Version 1.0",
        Description = "E-Commerce Store Api",
    });

    options.AddSecurityDefinition(
    JwtBearerDefaults.AuthenticationScheme,
    new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion

WebApplication? app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseApiVersioning();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.MapControllers();
app.Run();
