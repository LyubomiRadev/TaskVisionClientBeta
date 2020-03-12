using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskVisionClientBetaApp.Authentication;
using TaskVisionClientBetaApp.Data;

namespace TaskVisionClientBetaApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddAuthenticationCore();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            this.ConfigureAuthorizationServices(services);


            services.AddControllersWithViews();
            services.AddRazorPages();
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

            //code enables the serving of static files and the default file
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });


        }

        public void ConfigureAuthorizationServices(IServiceCollection services)
        {
            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy(Policies.Policies.IsAdmin, Policies.Policies.IsAdminPolicy());
            //    config.AddPolicy(Policies.Policies.IsDeveloper, Policies.Policies.IsDeveloperPolicy());
            //    config.AddPolicy(Policies.Policies.IsTester, Policies.Policies.IsTesterPolicy());
            //});

            services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();

            services.AddAuthorization(options =>
                {
                    options.AddPolicy("IsAdmin", policy =>
                 policy.Requirements.Add(new Policies.Policies.IsAdminRequirement(isAdmin: true)));
                });

           

            services.AddAuthorizationCore();
            services.AddScoped<TaskClientAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TaskClientAuthenticationStateProvider>());
        }



    }
}
