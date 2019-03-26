using ASPNETCORE.Infrastructure.AMQP;
using ASPNETCORE.Infrastructure.Notifications.Emitters;
using ASPNETCORE.Infrastructure.Notifications.Interfaces;
using ASPNETCORE.Repository;
using ASPNETCORE.Services.Clients.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCORE.Services.TeamService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<TeamDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("TeamDbContext")));

            var notificationServiceUrl = Configuration.GetSection("Services:notification.url").Value;
            services.AddSingleton<IHttpNotificationTeamServiceClient>(new HttpNotificationTeamServiceClient(notificationServiceUrl));

            services.Configure<Options>(Configuration.GetSection("AmqpOptions"));
            services.Configure<RoutingKeys>(Configuration.GetSection("AmpqRoutingKeys"));

            services.AddSingleton(typeof(INewTeamEventEmitter), typeof(NewTeamEventEmitter));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseMvc();
        }
    }
}
