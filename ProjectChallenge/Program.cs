using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddAutoMapper(cfg =>
{
    cfg.DisableConstructorMapping();
}, AppDomain.CurrentDomain.GetAssemblies());

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
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration["JwtToken"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddDbContext<ProjectContextDb>(options =>
{
    options.UseSqlServer(configuration["ConnectionString"],
                                        sqlServerOptionsAction: sqlOptions =>
                                        {
                                            sqlOptions.MigrationsAssembly(typeof(ProjectContextDb).GetTypeInfo().Assembly.GetName().Name);
                                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                        });
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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

app.UseCors("NgOrigins");

app.Run();
