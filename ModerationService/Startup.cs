using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModerationService.DAL;
using ModerationService.MessageHandlers;
using ModerationService.Services;
using Shared;
using Shared.Consul;

namespace ModerationService
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

            ConfigureConsul(services);

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

            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new AutoMapperProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMessagePublishing("ModerationService", builder =>
            {
                builder.WithHandler<NewProfileMessageHandler>("NewProfileMessage");
                builder.WithHandler<ForgetUserMessageHandler>("ForgetUserMessage");
                builder.WithHandler<DeleteTweetMessageHandler>("DeleteTweetMessage");
                builder.WithHandler<ProfileChangedMessageHandler>("ProfileChangedMessage");
                builder.WithHandler<NewProfanityTweetMessageHandler>("NewProfanityTweetMessage");
                builder.WithHandler<ProfileImageChangedMessageHandler>("ProfileImageChangedMessage");
            });

            services.Configure<ModerationContext>(Configuration.GetSection(nameof(ModerationContext)));
            services.AddSingleton<IModerationContext>(sp => sp.GetRequiredService<IOptions<ModerationContext>>().Value);

            services.AddScoped<IModerationService, Services.ModerationService>();
        }

        private void ConfigureConsul(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetServiceConfig();

            services.RegisterConsulServices(serviceConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
    }
}
