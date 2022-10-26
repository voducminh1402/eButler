using BusinessLogic.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eButler
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
            services.AddRazorPages();
            services.AddDbContext<eButlerContext>(options =>
            {
                string connectstring = Configuration.GetConnectionString("eButlerContext");
                options.UseSqlServer(connectstring);
            });
            services.AddScoped<UserService>();
            services.AddAuthentication( options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }              
                )
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/denied";
                    options.LogoutPath = "/logout";
                    options.Events = new CookieAuthenticationEvents()
                    {
                        OnSigningIn = async context =>
                        {
                            var scheme = context.Properties.Items.Where(k => k.Key == ".AuthScheme").FirstOrDefault();
                            var claim = new Claim(scheme.Key, scheme.Value);
                            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                            claimsIdentity.AddClaim(claim);
                            var userService = context.HttpContext.RequestServices.GetService<UserService>();
                            var nameIdentifier = claimsIdentity.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier)?.Value;
                            if (userService != null && nameIdentifier != null)
                            {
                                var user = userService.GetUserById(nameIdentifier);
                                if (user is null)
                                {
                                    user = userService.AddNewUser(claimsIdentity.Claims?.ToList());
                                }
                            }
                        }
                    };
                })
                .AddOpenIdConnect("google", options =>
                {
                    options.Authority = "https://accounts.google.com";
                    IConfigurationSection configuration = Configuration.GetSection("Authentication:Google");
                    options.ClientId = configuration["ClientId"];
                    options.ClientSecret = configuration["ClientSecret"];
                    options.CallbackPath = "/auth";
                    options.SaveTokens = true;
                    options.Events = new OpenIdConnectEvents()
                    {
                        OnTokenValidated = async context =>
                        {
                            if (context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "114702882049473722316")
                            {
                                var claim = new Claim(ClaimTypes.Role, "admin");
                                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                                claimsIdentity.AddClaim(claim);
                            }

                        }
                    };
                })
                //.AddGoogle(googleOption =>
                //{
                //    IConfigurationSection configuration = Configuration.GetSection("Authentication:Google");
                //    googleOption.ClientId = configuration["ClientId"];
                //    googleOption.ClientSecret = configuration["ClientSecret"];
                //    googleOption.CallbackPath = "/auth";
                //    googleOption.AuthorizationEndpoint += "?promp=consent";
                //})
                ;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
