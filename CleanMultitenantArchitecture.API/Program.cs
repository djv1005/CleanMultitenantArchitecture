using CleanMultitenantArchitecture.API.Handlers;
using CleanMultitenantArchitecture.Aplication.Jwt;
using CleanMultitenantArchitecture.Domain.IRepositories;
using CleanMultitenantArchitecture.Infraestructure.Data;
using CleanMultitenantArchitecture.Infraestructure.Repositories;
using CleanMultitenantArchitecture.Infraestructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
     .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
         options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // Optional: Use camelCase for property names
     });

var config = builder.Configuration;

var secret = config.GetSection("JWT")["Secret"].ToString();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
              ValidateIssuer = false,
              ValidateAudience = false,
          };
      });





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton(config);
builder.Services.AddDbContext<OrganizationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("OrganizationConnectionString")));


builder.Services.AddDbContext<ProductDbContext>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMigrationTenantScriptService, MigrationTenantScriptService>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITenantService, TenantService>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new AuthorizedRolesRequirement("admin"));
    });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthorizationHandler, AuthorizedRolesHandler>();





var app = builder.Build();

app.UseMiddleware<TenantMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/", () => "Hello World!");

app.Run();
