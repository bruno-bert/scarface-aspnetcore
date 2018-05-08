using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using <%= data.schema.appconfig.name %>.Infrastructure;
using <%= data.schema.appconfig.name %>.Infrastructure.Filters;
using <%= data.schema.appconfig.name %>.Models;

namespace <%= data.schema.appconfig.name %> {
    public class Startup {
     
      
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationBuilder _builder;
        private  string _basePath;      

        public IConfigurationRoot Configuration { get; }

        public AppConfig appConfig;

        

        public Startup (IHostingEnvironment env) {

            _basePath = env.ContentRootPath;

            _builder = new ConfigurationBuilder ()
                .SetBasePath (_basePath)
                .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true)
                .AddEnvironmentVariables ();
            Configuration = _builder.Build ();
            _env = env;
        }

     

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            var cs = Configuration.GetConnectionString ("DBConnectionString");
            var config = Configuration.GetSection("AppConfig");

            services.Configure<AppConfig>(config);
            services.AddScoped<ConditionalRequireHttpsAttribute>();

            appConfig = new AppConfig();
            config.Bind(appConfig);
            
           
            services.AddCors (o => o.AddPolicy (appConfig.securityOptions.policyName, builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));

            

       
            services.AddMvc (
                
                options => { 
                  //  options.Filters.Add(new RequireHttpsAttribute());
                    options.Filters.Add(typeof(CustomExceptionFilter));
                })

                .AddJsonOptions (options => {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                
                .Services
                .AddSwaggerGen(c => {
                      c.SwaggerDoc (appConfig.apiVersion, new Info { Title = appConfig.apiTitle, Version = appConfig.apiVersion });
                         var filePath = System.IO.Path.Combine(_basePath, appConfig.apiDocsPath);
                         c.IncludeXmlComments(filePath);
                      
            });

            
            //Adds database context
            //services.AddDbContext<AppDBContext> (options => options.UseSqlServer (cs));
            services.AddDbContext<AppDBContext> (options => options.UseNpgsql  (cs));

           //Adds mapping from resources to domain and vice-versa 
           services.AddAutoMapper();
           

            //Adds Services Repositories
            services.AddScoped (typeof (IRepository<>), typeof (EFRepository<>));
            services.AddScoped (typeof (IAsyncRepository<>), typeof (AsyncEFRepository<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole (Configuration.GetSection ("Logging"));
            loggerFactory.AddDebug ();

            app.UseStaticFiles ();

            app.UseCors (appConfig.securityOptions.policyName);

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler (
                    builder => {
                        builder.Run (
                            async context => {
                                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                                context.Response.Headers.Add ("Access-Control-Allow-Origin", "*");

                                var error = context.Features.Get<IExceptionHandlerFeature> ();
                                if (error != null) {
                                    context.Response.AddApplicationError (error.Error.Message);
                                    await context.Response.WriteAsync (error.Error.Message).ConfigureAwait (false);
                                }
                            });
                    });
            }

            app.UseMvc ();
            app.UseSwagger ();

            //This redirects any http request to https
            if  (appConfig.securityOptions.useSsl)
             app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301,appConfig.securityOptions.sslPort));

            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint (String.Concat("/swagger/",appConfig.apiVersion,"/swagger.json"), appConfig.apiTitle);
            });

        }
    }
}