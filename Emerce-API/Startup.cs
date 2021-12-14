using AutoMapper;
using Emerce_API.Infrastructure;
using Emerce_Service.Category;
using Emerce_Service.Product;
using Emerce_Service.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Emerce_API
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string AllowAllHeaders = "_allowAllHeaders";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper();
            services.AddSingleton(mapper);
            //services for user,product,category
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            //memory cache
            services.AddMemoryCache();
            services.AddScoped<LoginFilter>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowAllHeaders,
                                  builder =>
                                  {
                                      builder/*.WithOrigins("http://localhost:3001/")*/
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin();
                                  });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Emerce_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Emerce_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseCors(AllowAllHeaders);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
