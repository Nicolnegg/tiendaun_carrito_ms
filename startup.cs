
using Microsoft.EntityFrameworkCore;
using Proyecto_Carrito.Data;


public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
        //var connectionString = "server=localhost;port=3306;database=tiendaun_carrito;user=root;password=;SslMode=none;Max Pool Size=300";
        var connectionString = Configuration.GetConnectionString("MySqlConnection");

            services.AddDbContext<MyDbContext>(
                dbContextOptions => dbContextOptions
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.EnableRetryOnFailure(
                    maxRetryCount: 15,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                )
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
