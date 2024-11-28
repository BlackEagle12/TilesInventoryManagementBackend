using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core
{
    public static class CoreDependencies
    {
        public static void InjectCoreDependencies(this IServiceCollection services, IConfiguration configuration, string corsPolicy)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            services.AddJWT(configuration);

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: corsPolicy,
                                  policy =>
                                  {
                                      policy.WithOrigins(appSettings!.ClientList)
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .WithExposedHeaders("Content-Disposition");
                                  });
            });
        }

        public static void AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();


            var key = Encoding.ASCII.GetBytes(appSettings!.SecurityKey!);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = appSettings.APIUrl!,
                    ValidAudiences = appSettings!.ClientList!,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //services.AddAntiforgery();


        }
    }
}
