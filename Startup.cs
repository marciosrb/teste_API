using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data;
using ProductCatalog.Repositories;
using System;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

namespace ProductCatalog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddResponseCompression();

            services.AddScoped<StoreDataContext, StoreDataContext>();
            services.AddTransient<ProductRepository, ProductRepository>();
             
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Teste API",
        Description = "Simples ASP.NET Core Web API",
       
        Contact = new OpenApiContact
        {
            Name = "Márcio Ribeiro",
            Email = "urbanizeprojetos@gmail.com",
            Url = new Uri("https://github.com/marciovisualsistemas/"),
        },
        
    });
});

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
	        app.UseSwaggerUI(c =>
	{
		    c.RoutePrefix = "swagger";
		    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste API");
	});

            app.UseMvc();
            app.UseResponseCompression();
        }
    }
}