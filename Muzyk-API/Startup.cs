using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Muzyk_API.Data;
using Muzyk_API.Helpers;
using Muzyk_API.Hubs;

namespace Muzyk_API
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
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 200 * 1024 * 1024;
                options.ValueCountLimit = 200 * 1024 * 1024;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.BuildServiceProvider().GetService<DataContext>().Database.Migrate();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                builder.WithOrigins("http://localhost:5000/").AllowAnyHeader().AllowAnyMethod());
            });
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper();
            services.AddTransient<Seed>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IMuzykRepository, MuzykRepository>(); 
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();  
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                          .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddSignalR();
            services.AddScoped<LogUserActivity>();
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 200 * 1024 * 1024;
                options.ValueCountLimit = 200 * 1024 * 1024;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                builder.WithOrigins("http://localhost:5000/").AllowAnyHeader().AllowAnyMethod());
            });
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper();
            services.AddTransient<Seed>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IMuzykRepository, MuzykRepository>();
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                          .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddSignalR();
            services.AddScoped<LogUserActivity>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            //seeder.seedUsers();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Fallback", action = "Index" }
                );
            });
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/mHub");
            });
        }
    }
}
