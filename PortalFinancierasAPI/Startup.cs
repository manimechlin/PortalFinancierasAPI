using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Services.Hashing;
using BusinessEntities.Token;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Auth;
using Services.Catalog;
using Services.Email;
using Services.installationRequest;
using Services.Interfaces;
using Services.Token;
using Services.User;
using Services.SendEmail;
using Services.Report;
using Services.Location;
using Services.Configuration;
using PortalFinancierasAPI.Handler;
using Services.ImageWriter;

namespace PortalFinancierasAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(); // Make sure you call this previous to AddMvc

            services.AddScoped(typeof(IDBHelper), typeof(DBHelper));
            services.AddScoped(typeof(IEmailRepository), typeof(EmailRepository));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));
            services.AddScoped(typeof(ICatalogRepository), typeof(CatalogRepository));
            services.AddScoped(typeof(ICatalogService), typeof(CatalogService));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));


            services.AddScoped(typeof(IinstallationRepository), typeof(installationRepository));
            services.AddScoped(typeof(IinstallationService), typeof(installationService));

            services.AddScoped(typeof(ISendmailRepository), typeof(SendmailRepository));
            services.AddScoped(typeof(ISendEmailService), typeof(SendEmailService));

            services.AddScoped(typeof(IReportRepository), typeof(ReportRepository));
            services.AddScoped(typeof(IReportService), typeof(ReportService));

            services.AddScoped(typeof(ILocationRepository), typeof(LocationRepository));
            services.AddScoped(typeof(ILocationService), typeof(LocationService));

            services.AddScoped(typeof(IConfigurationDbRepository), typeof(ConfigurationDbRepository));
            services.AddScoped(typeof(IConfigurationDbService), typeof(ConfigurationDbService));

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenHandler, TokenHandler>();
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<IImageWriter, ImageWriter>();

            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = signingConfigurations.Key,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Make sure you call this before calling app.UseMvc()
            app.UseCors(
                options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            );

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
