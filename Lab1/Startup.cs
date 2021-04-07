using System;
using FluentValidation.AspNetCore;
using Lab1.Filters;
using Lab1.Infrastructure;
using Lab1.Interfaces;
using Lab1.Interfaces.SqlRepositories;
using Lab1.Interfaces.SqlServices;
using Lab1.Mappers;
using Lab1.Repositories.SQLRepositories;
using Lab1.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Lab1
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
            services.AddDbContext<EfConfig.MyDbContext>(
                options => options.UseMySql(
                        Configuration.GetConnectionString("DefaultConnection"),
                        new MySqlServerVersion(new Version(8, 0, 0)))
                    .EnableSensitiveDataLogging()
                    .EnableSensitiveDataLogging()
            );
            services.AddControllers(options =>
                options.Filters.Add(new ValidationFilter())
            ).AddFluentValidation(options =>
                options.RegisterValidatorsFromAssemblyContaining<Startup>()
            ).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Lab1", Version = "v1"}); });

            #region AutoMapper

            services.AddAutoMapper(
                typeof(UserProfile),
                typeof(StationProfile),
                typeof(TrainProfile),
                typeof(TicketProfile),
                typeof(StoppageProfile),
                typeof(RouteProfile));

            #endregion


            #region SQL repositories

            services.AddTransient<IStationRepository, StationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRouteRepository, RouteRepository>();
            services.AddTransient<IStoppageRepository, StoppageRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<ITrainRepository, TrainRepository>();

            #endregion

            #region SQL services

            services.AddTransient<IStationService, StationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRouteService, RouteService>();
            services.AddTransient<IStoppageService, StoppageService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITrainService, TrainService>();

            #endregion

            services.AddTransient<IConnectionFactory, ConnectionFactory>();

            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}