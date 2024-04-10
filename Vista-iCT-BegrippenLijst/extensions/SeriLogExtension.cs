using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extensions
{
    public static class SeriLogExtension
    {
        /// <summary>
        /// Extension that adds/ Configures serilog.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            //Creates and configures serilog.
            //Uses dependency injection from the serilog.json for the settings.
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            return services;
        }
    }
}
