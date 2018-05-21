using DAO;
using DAO.Extensions;
using FluentValidation.AspNetCore;
using Grace.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;
using WebUI.Controllers;
using WebUI.Filters;

namespace WebUI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            if (!CurrentEnvironment.IsDevelopment())
            {
                builder.AddMvcOptions(o => o.Filters.Add(typeof(GlobalExceptionFilter)));
            }
            builder.AddMvcOptions(o => o.Filters.Add(typeof(ValidatorActionFilter)));
            builder.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            // secretKey contains a secret passphrase only your server knows
            var secretKey = Configuration["Jwt:Key"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.SaveToken = true;
                });
        }

        public void ConfigureContainer(IInjectionScope scope)
        {
            //TODO this is fix for run on Linux, this should be deleted when will be used dotnet core 2.1
            scope.Configure(c =>
                c.ExcludeTypeFromAutoRegistration("Microsoft.AspNetCore.DataProtection.RegistryPolicyResolver"));


            scope.Registrate(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapAppRoutes();
            });

            app.ApplicationServices.Migrate();

            if (env.IsDevelopment() || env.IsStaging())
            {
                app.ApplicationServices.Seed();
            }
        }
    }
}
