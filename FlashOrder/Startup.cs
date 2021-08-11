using AutoMapper;
using FlashOrder.Configurations;
using FlashOrder.Data;
using FlashOrder.IRepository;
using FlashOrder.Policies;
using FlashOrder.Repository;
using FlashOrder.Services;
using FlashOrder.Services.Auth;
using FlashOrder.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FlashOrder
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FlashOrder", Version = "v1"});
            });
            
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", 
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            
            
            services.AddDbContext<DatabaseContext>(options=>
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));


            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.AddAuthorization(options =>
            {
                // options.AddPolicy("OnlyChefOfRecipe", policy => policy.RequireClaim("Recipe"));
                // options.AddPolicy("OnlyChefOfRecipe",
                //     policy => policy.Requirements.Add(new OnlyChefOfRecipeRequirement()));
                
                options.AddPolicy("CreatorChefPolicy",
                    policy => policy.Requirements.Add(new CreatorChefRequirement()));
            });
            
            services.AddAutoMapper(typeof(MapperInitializer));

            
            //every time is needed ,a new instance is created ,it is similar to service provider in laravel
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
 
            services.AddSingleton<IAuthorizationHandler, CreatorChefHandler>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddTransient<IHostedService, RecipeRatingService>();
            services.AddScoped(typeof(MyUtils));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlashOrder v1"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            //this help us know the host current host url
            app.UseHttpContext();
            
            
            //using cors with the policy "AllowAll"
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}