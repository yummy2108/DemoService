using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using DemoService.Models;
using DemoService.Data;
using Microsoft.EntityFrameworkCore;
using DemoService.Service;
using Microsoft.Azure.Cosmos;

namespace DemoService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection, string CosmosConnectionString)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];

            var client = new CosmosClient(CosmosConnectionString);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            var cosmosDbService = new CosmosDbService(client, databaseName, containerName);
            return cosmosDbService;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddScoped<IMyServiceRepo, MyServiceSimpleRepo>();
            services.AddDbContext<MyServiceContext>(opt =>
                                    opt.UseInMemoryDatabase("MyServiceList"));
            var CosmosConnectionString = Configuration.GetValue<string>(Configuration["KeyVault:ConnectionStringSecretId"]);
            services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb"),CosmosConnectionString).GetAwaiter().GetResult());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
