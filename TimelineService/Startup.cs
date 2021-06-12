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
using Shared;
using Shared.Consul;
using TimelineService.DAL;
using TimelineService.MessageHandlers;
using TimelineService.Services;

namespace TimelineService
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

            services.AddAuthorization();

            services.AddMessagePublishing("TimelineService", builder =>
            {
                builder.WithHandler<NewProfileMessageHandler>("NewProfileMessage");
                builder.WithHandler<ForgetUserMessageHandler>("ForgetUserMessage");
                builder.WithHandler<AddFollowerMessageHandler>("AddFollowerMessage");
                builder.WithHandler<UnApproveTweetMessageHandler>("UnApproveTweetMessage");
                builder.WithHandler<NewPostedTweetMessageHandler>("NewPostedTweetMessage");
                builder.WithHandler<ProfileChangedMessageHandler>("ProfileChangedMessage");
                builder.WithHandler<RemoveFollowerMessageHandler>("RemoveFollowerMessage");
                builder.WithHandler<ProfileImageChangedMessageHandler>("ProfileImageChangedMessage");
            });

            services.Configure<TimelineContext>(Configuration.GetSection(nameof(TimelineContext)));
            services.AddSingleton<ITimelineContext>(sp => sp.GetRequiredService<IOptions<TimelineContext>>().Value);

            services.AddScoped<ITimelineService, Services.TimelineService>();
        }

        private void ConfigureConsul(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetServiceConfig();

            services.RegisterConsulServices(serviceConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipelines.
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
