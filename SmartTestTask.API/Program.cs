using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using SmartTestTask.API.Authentication;
using SmartTestTask.API.Constants;
using SmartTestTask.API.Extensions;
using SmartTestTask.API.Middlewares;
using SmartTestTask.Application;
using SmartTestTask.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartTestTask", Version = "v1" });
    options.CustomSchemaIds(type => type.ToString());
    options.AddSecurityDefinition("ApiKeyAuth", new OpenApiSecurityScheme {
        Type = SecuritySchemeType.ApiKey,
        Name = "ApiKey",
        Scheme = "apikey",
        Description = "Please insert API Key string into field",
        In = ParameterLocation.Header
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKeyAuth" 
                } 
            },
            new [] {"SmartTestTask"}
        } 
    });
});

builder.Services
    .AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
        CustomAuthenticationSchemes.ApiKeyAuthentication, options => { });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(CustomAuthenticationPolicies.ApiKeyAuthenticationPolicy,
        new AuthorizationPolicyBuilder(CustomAuthenticationSchemes.ApiKeyAuthentication).RequireAuthenticatedUser()
            .Build());
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.SeedDatabaseAsync();
app.Run();
