using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using <%= data.schema.appconfig.name %>.Infrastructure;

namespace <%= data.schema.appconfig.name %> {
    public class Program {
        
       
        private static string certificatePath;

        private static AppConfig appConfig;
        private static string _basePath;    

        public static void Main (string[] args) {

           _basePath = System.IO.Directory.GetCurrentDirectory ();

            var config = new ConfigurationBuilder ()
                .SetBasePath (_basePath)
                .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile ("appsettings.Development.json", optional : true)
                .Build();

            appConfig = new AppConfig();
            config.GetSection("AppConfig").Bind(appConfig);    

            certificatePath = System.IO.Path.Combine (_basePath, appConfig.securityOptions.sslCertificateFile);

            BuildWebHost (args).Run ();
        }

        public static IWebHost BuildWebHost (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ()
            .UseKestrel (options => {
                options.Listen (IPAddress.Loopback, 5000);
                options.Listen (IPAddress.Loopback, appConfig.securityOptions.sslPort, listenOptions => {
                    listenOptions.UseHttps (certificatePath, Helper.Decrypt(appConfig.securityOptions.sslPassword));
                });
            })
            .Build ();
    }
}