using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using ProjectChallengeAPI.Filter;
using ProjectChallengeAPI.Middlewares;
using ProjectChallengeData.Database;
using ProjectChallengeData.Database.Repositories;
using ProjectChallengeData.Database.Repositories.IRepositories;
using ProjectChallengeDomain.IService;
using ProjectChallengeDomain.Models.Requests;
using ProjectChallengeService.Services;
using ProjectLibraryData.Database.Repositories;
using ProjectLibraryData.Database.Repositories.IRepositories;
using ProjectLibraryDomain.IService;
using ProjectLibraryService.Services;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
//repositories
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

//services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUSerService, UserService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1",
       new OpenApiInfo
       {
           Title = "Library API",
           Version = "v1",
           Description = "Library API",

       });

});
builder.Services.AddDbContext<ProjectContextDb>(options =>
{
    options.UseInMemoryDatabase("LibraryDb")
        .EnableSensitiveDataLogging()
        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidationFilter));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware(typeof(GlobalExceptionHandlingMiddleware));

app.MapControllers();
app.UseSwagger(c =>
{
    c.RouteTemplate = "/swagger/swagger/{documentname}/swagger.json";
});

app.UseSwaggerUI(c =>
{

    c.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
});

app.Run();
