using System;
using System.Net;
using System.Net.Http;
using AutoMapper;
using BusinessLogic.Queries.GetCalendarQuery;
using BusinessLogic.Queries.GetCalendarQuery.InternalModels;
using BusinessLogic.Queries.GetFeedQuery;
using BusinessLogic.Queries.GetFeedQuery.InternalModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebUi.Data;
using WebUi.Models.Calendar;
using WebUi.Models.Feed;

namespace WebUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsecrets.deploy.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            BusinessLogic.SecretsManager.VkApiKey = Configuration["VkApiUserToken"];
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAutomapper(services);

            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy(new Uri("http://10.0.25.3:8080")) { UseDefaultCredentials = true },
                DefaultProxyCredentials = CredentialCache.DefaultCredentials
            };
            var httpClient = new HttpClient(handler);
            services.AddSingleton(httpClient);

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<GetCalendarQuery>();
            services.AddSingleton<GetFeedQuery>();

            services.AddSingleton<CalendarService>();
            services.AddSingleton<FeedService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void ConfigureAutomapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CalendarEntity, CalendarModel>();
                cfg.CreateMap<CalendarDayEntity, CalendarDayModel>();
                cfg.CreateMap<CalendarEventEntity, CalendarEventModel>();
                cfg.CreateMap<CalendarLegendItemEntity, CalendarLegendItemModel>();
                cfg.CreateMap<PostEntity, PostModel>();
            });
            services.AddSingleton<IMapper>(new Mapper(config));
        }
    }
}