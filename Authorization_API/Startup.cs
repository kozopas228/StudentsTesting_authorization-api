using Authorization_Data;
using Authorization_Data.Implementation;
using Authorization_Data.Interfaces;
using Authorization_Models;
using Authorization_Services.Implementation;
using Authorization_Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Collections;

namespace Authorization_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _currentEnvironment;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AddAuthCredentials(services);

            services.AddSwaggerGen();

            AddDb(services);
            ConfigureDependencies(services);
        }

        public virtual void ConfigureDependencies(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITestAttemptCrudService, TestAttemptCrudService>();
            services.AddTransient<IUserCrudService, UserCrudService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITestAttemptRepository, TestAttemptRepository>();
        }

        private void AddAuthCredentials(IServiceCollection services)
        {
            if (_currentEnvironment.IsEnvironment("Testing"))
            {
                var conf = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["Auth:Issuer"] = "authApi",
                        ["Auth:Audience"] = "gatewayApi",
                        ["Auth:Secret"] = "Super-Secret-Key-123",
                        ["Auth:TokenLifetime"] = "3600",

                    })
                    .Build();
                services.Configure<AuthOptions>(conf);
            }
            else
            {
                services.Configure<AuthOptions>(Configuration.GetSection("Auth"));
            }
        }

        private void AddDb(IServiceCollection services)
        {
            if (_currentEnvironment.IsEnvironment("Testing"))
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
