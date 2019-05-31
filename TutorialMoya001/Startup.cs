using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TutorialMoya001.Hubs;
using TutorialMoya001.Repositories;
using TutorialMoya001.Repositories.Interfaces;
using TutorialMoya001.Utils;

namespace TutorialMoya001
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["ApiAuth:Issuer"],
                    ValidAudience = Configuration["ApiAuth:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApiAuth:SecretKey"]))
                };
            });
            var AzureBlobConnectionString = Configuration.GetValue<string>("AzureBlobConnectionString");
            services.AddTransient<IImagenesRepository>(f =>
                new ImagenesRepository(AzureBlobConnectionString)
            );
            var AzureTableConnectionString = Configuration.GetValue<string>("AzureTablesConnectionString");
            services.AddTransient<IUsersRepository>(f =>
                new UserRepository(AzureTableConnectionString)
            );
            services.AddTransient<IDevicesRepository>(f =>
                new DeviceRepository(AzureTableConnectionString)
            );
            var IsUser = Configuration["ApiAuth:Issuer"];
            var Audience = Configuration["ApiAuth:Audience"];
            var SecretKey = Configuration["ApiAuth:SecretKey"];
            services.AddTransient(f =>
                new Util(IsUser, Audience, SecretKey)
            );
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            /*
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                       .WithOrigins("http://localhost:8080", "http://localhost:80")
                       .AllowCredentials();
            }));
            */
            services.AddCors();
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            
            app.UseCors(builder =>
               builder
                   .WithOrigins("http://localhost:8080", "http://localhost:80", "http://68.183.127.101", "http://ropalinda.website", "http://ropalinda.website")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   );
                   
            // Register SignalR hubs
            app.UseSignalR(route =>
            {
                route.MapHub<ActionHub>("/action-hub");
            });
            // app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
