using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.DAL;
using AuthenticationService.MessageHandlers;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace AuthenticationService
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
            services.AddControllers();

            var secret = "eBCatxoffIIq6ESdrDZ8LKI3zpxhYkYM";
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();

            services.AddMessagePublishing("AuthenticationService", builder =>
            {
                builder.WithHandler<RegisterNewUserMessageHandler>("NewUserMessage");
                builder.WithHandler<EmailChangedMessageHandler>("EmailChangedMessage");
            });

            services.AddScoped<IAuthService, AuthService>();

            services.AddDbContext<AuthenticationContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            MigrateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void MigrateDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<AuthenticationContext>();

            var relationalDatabaseCreator =
                (RelationalDatabaseCreator)dbContext.Database.GetService<IDatabaseCreator>();

            var databaseExists = relationalDatabaseCreator.Exists() && relationalDatabaseCreator.HasTables();

            dbContext.Database.Migrate();

            if (!databaseExists)
            {
                //TODO: add standard data
            }
        }
    }
}
