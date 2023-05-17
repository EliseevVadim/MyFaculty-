using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFaculty.Identity.Configurations;
using MyFaculty.Identity.Data;
using MyFaculty.Identity.Middleware;
using MyFaculty.Identity.Models;
using MyFaculty.Identity.Services;
using System;

namespace MyFaculty.Identity
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 11)));
            });
            services.AddIdentity<AppUser, IdentityRole<int>>(config =>
            {
                config.Password.RequiredLength = 8;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.User.RequireUniqueEmail = true;
                config.User.AllowedUserNameCharacters = "'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+";
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole<int>>();
            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "MyFaculty.Identity.Cookie";
                config.LoginPath = "/auth/login";
                config.LogoutPath = "/auth/logout";
            });
            services.AddRolesManager();
            services.AddCors(options =>
            {
                options.AddPolicy("InitialPolicy", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseMiddleware<PublicFacingUrlMiddleware>(_configuration.GetValue<string>("BaseUrl"));
            app.UseCors("InitialPolicy");
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
