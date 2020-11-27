using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
namespace PATCH
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "FoodFlow API",
                    Description = "FoodFlow server API.",
                    Contact = new OpenApiContact()
                    {
                        Name = "VReal Soft",
                        Email = "andrey.s@vrealsoft.com",
                        Url = new Uri("https://vrealsoft.com/", UriKind.Absolute),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Use under License",
                        Url = new Uri("https://vrealsoft.com/", UriKind.Absolute)
                    }
                });
                #region IncludeAllDocumentary

                var xmlAssembly = Path.Combine(AppContext.BaseDirectory, "Patch.xml");
                //var xmlBLL = Path.Combine(AppContext.BaseDirectory, "FoodFlow.BLL.xml");
                //var xmlDAL = Path.Combine(AppContext.BaseDirectory, "FoodFlow.DAL.xml");

                //options.IncludeXmlComments(xmlBLL);  
                //options.IncludeXmlComments(xmlDAL);
                options.IncludeXmlComments(xmlAssembly);

                #endregion

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description =
                            "JWT Authorization header using the Bearer scheme. " +
                            "\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below." +
                            "\r\n\r\nExample: \"Bearer ey...\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                options.DescribeAllEnumsAsStrings();
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "FoodFlow API V1");
            });
            
            app.UseHttpsRedirection();
            app.UseMvc();

        } 
    }
}
