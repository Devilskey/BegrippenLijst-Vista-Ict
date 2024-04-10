using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions
{
    public static class CorsExtension
    {
       /// <summary>
       /// Sets up a cors policy corsany
       /// </summary>
       /// <param name="services"></param>
       /// <returns></returns>
        public static IServiceCollection SetupCorseAny(this IServiceCollection services)
        {
            //Setsup the corse policy Corse any and configures it the accept api calls from any origin.
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", Policy =>
                    Policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            return services;
        }

        /// <summary>
        /// Enables the corse policy
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder EnableCorse(this IApplicationBuilder app)
        {
            app.UseCors("Cors");
            return app;
        }
    }
}