using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            // Specifically creating these so they are initialised ASAP
            SettingsService settingsService = new SettingsService();
            EliteApiService eliteApiService = new EliteApiService(settingsService);
            
            services.AddSingleton(sp => settingsService);
            services.AddSingleton(sp => eliteApiService);
            services.AddSingleton<RoutePlanningService>();
            services.AddSingleton<VirtualControlsService>();

            services.AddHttpClient<IRoadToRichesRouteService, RoadToRichesRouteService>(client => {
                client.BaseAddress = new Uri("https://spansh.co.uk");
                client.MaxResponseContentBufferSize = 256000;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /// 
            /// SPA / Host Files
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            ///
            /// API
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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
