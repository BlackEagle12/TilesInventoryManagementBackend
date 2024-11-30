using API;
using Core;
using Core.CustomExceptionFilter;
using Data;
using Dto;
using Mapper;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Repo;
using Service;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;
using System.Security.Claims;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

IConfigurationSection appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

var emailConfigurationSection = builder.Configuration.GetSection("EmailConfigurations");
builder.Services.Configure<EmailConfigurations>(emailConfigurationSection);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    //options.Filters.Add<ExceptionFilter>();

}).AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.Culture = CultureInfo.InvariantCulture;
    opt.SerializerSettings.DateFormatString = "dddd, dd, MMMM, yyyy hh:mm:ss tt K";
    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "TilesInventorySystem.API",
        Version = "v1",
    });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Enter a Valid Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.InjectCoreDependencies(builder.Configuration, MyAllowSpecificOrigins);
builder.Services.InjectDBContextDependencies(builder.Configuration.GetConnectionString("Online")!);
builder.Services.InjectRepoDependencies();
builder.Services.InjectServiceDependencies();
builder.Services.InjectMapperDependnecies();

builder.Services.AddScoped<UserDto>(provider =>
{
    UserDto dto = null;
    try
    {
        var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
        if (httpContext?.Request != null)
        {
            var userClaim = httpContext?.User?
                          .Claims?.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.UserData))?.Value;

            if (userClaim != null)
                dto = JsonConvert.DeserializeObject<UserDto>(userClaim)!;
        }
    }
    catch (Exception)
    {
        dto = null;
    }

    return dto!;
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.DocumentTitle = "TilesInventoryManagement";
        o.DocExpansion(DocExpansion.None);
        o.EnableFilter();
        o.EnableTryItOutByDefault();
    });


app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
