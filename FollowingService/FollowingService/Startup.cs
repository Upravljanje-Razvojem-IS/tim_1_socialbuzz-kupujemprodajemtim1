using CommunicationKeyAuthClassLibrary;
using FluentValidation;
using FluentValidation.AspNetCore;
using FollowingService.Data;
using FollowingService.Data.Interfaces;
using FollowingService.Data.Repositories;
using FollowingService.Entities;
using FollowingService.MockServices.BlackList;
using FollowingService.MockServices.UserService;
using FollowingService.Validators;
using LoggingClassLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FollowingService
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
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();

            services.AddDbContext<FollowingContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<ILoggerProvider, LoggerProvider>();

            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IUserMockRepository, UserMockService>();
            services.AddScoped<IBlackListMockRepository, BlackListMockService>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("FollowingOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Follow API",
                        Version = "1",
                        Description = "This API is used for creating, updating, deleting and reviewing follows.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Darko Karpic",
                            Email = "karpicdarko1@gmail.com"
                        }
                    });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                setupAction.IncludeXmlComments(xmlCommentsPath);

            });

            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<Follow>, FollowValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ILoggerProvider loggerProvider, ILogger logger)
        {
            loggerFactory.AddProvider(loggerProvider);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An error has occurred. Please try again.");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/FollowingOpenApiSpecification/swagger.json", "Transaction API");
                setupAction.RoutePrefix = "";
            });

            app.UseMiddleware<CommunicationKeyAuthMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
