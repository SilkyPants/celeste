using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Celeste.Services;
using Celeste.Services.Spanch;
using System;

namespace Celeste
{
    public class CelesteApi
    {
        public CelesteApi(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            //// Specifically creating these so they are initialised ASAP
            SettingsService settingsService = new SettingsService();
            EliteApiService eliteApiService = new EliteApiService(settingsService);

            services.AddSingleton(sp => settingsService);
            services.AddSingleton(sp => eliteApiService);
            services.AddSingleton<RoutePlanningService>();

            services.AddHttpClient<IRoadToRichesRouteService, RoadToRichesRouteService>(client =>
            {
                client.BaseAddress = new Uri("https://spansh.co.uk");
                client.MaxResponseContentBufferSize = 256000;
            });

            // services.AddControllers();
            services.AddControllersWithViews()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.IgnoreNullValues = true;
             });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "client-app/dist";
            });
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

            /// 
            /// SPA / Host Files
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            ///
            /// API
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "client-app";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            ///
            /// Websocket stuff
            // app.UseWebSockets();

            // app.Use(async (context, next) =>
            // {
            //     if (context.WebSockets.IsWebSocketRequest)
            //     {
            //         WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            //         Console.WriteLine("WebSocket Connected");
            //     }
            //     else
            //     {
            //         await next();
            //     }
            // });
        }
    }
}
