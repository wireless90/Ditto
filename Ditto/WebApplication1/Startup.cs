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

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Cookie.HttpOnly = false;

                    x.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                    x.Events.OnSignedIn = (sd) =>
                    {
                        var xs = sd.Principal;
                        return Task.CompletedTask;
                    };
                });

            services.AddDitto(new DittoOptions()
            {
                Transforms = new TransformBuilder()
                                .AddNewTransform("TP")
                                    .AddClaim(ClaimTypes.Name, "Tan Tong Peng")
                                .AddNewTransform("HJ")
                                    .AddClaim(ClaimTypes.Name, "Wong Hejun")
                                .Build()
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string wrongConnectionString = @"Data Source=DESKTOP-S0OLDPR\SQLEXPRESS;Initial Catalog=VidlyDB;Integrated Security=True";
            services.AddDbContext<CompanyDbContext>(options => options.UseSqlServer(wrongConnectionString));

            services.AddApplication()
                .AddPersistence();

            services.AddSwaggerGen();


            return ContainerConfig.CreateAutofacServiceProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseDitto();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
