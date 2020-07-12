using AutoMapper;
using CadastroProduto.Application;
using CadastroProduto.CQS;
using CadastroProduto.Domain;
using CadastroProduto.Infra;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CadastroProduto.Api.Principal
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddDbContext<CadastroProdutoContext>(options => options.UseNpgsql(Configuration["ConnectionString"]));

            services.AddAutoMapper(x => x.CreateMap<Produto, ProdutoDto>(), AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(AtualizarProdutoCommandHandler).GetTypeInfo().Assembly);


            services.AddScoped<IProdutoQueries, ProdutoQueries>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ProdutoCommandValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cadastro de produtos", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "CadastroProduto.Api.Principal.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CadastroProdutoContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadastro de produto");
            });
        }
    }
}
