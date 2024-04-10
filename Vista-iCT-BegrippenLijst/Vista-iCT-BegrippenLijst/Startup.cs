using Serilog;
using Extensions;
using extensions;

namespace Vista_iCT_BegrippenLijst
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.SetupCorseAny();

            services.AddControllers();

            services.AddAuthorization();

            services.AddSwaggerGen();

            services.AddMvc();

            services.AddEndpointsApiExplorer();

            services.AddSerilog(Configuration);

            services.AddSwaggerSecurityConfiguration();

            services.AddAuthTokenNeeded();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.EnableCorse();

            app.UseSerilogRequestLogging();

            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

       //     app.UseDatabaseMigration();
       //     app.UseDatabaseMigration();

            app.UseSwaggerDocumentation();

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}