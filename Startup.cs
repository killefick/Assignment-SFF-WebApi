using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFF.Context;
using SFF.Models;

namespace SFF
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
            services.AddDbContext<myDbContext>(opt =>
            opt.UseSqlite("Data Source = SFF_DB.db;"));


            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddAutoMapper(typeof(Startup));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperConfig>();

                // cfg.CreateMap<Movie, MovieDto>();
                // cfg.CreateMap<Rating, RatingDto>();
                // cfg.CreateMap<Rental, RentalDto>();
                // cfg.CreateMap<Studio, StudioDto>();
                // cfg.CreateMap<Trivia, TriviaDto>();

                // cfg.CreateMap<MovieDto, Movie>();
                // cfg.CreateMap<RatingDto, Rating>();
                // cfg.CreateMap<RentalDto, Rental>();
                // cfg.CreateMap<StudioDto, Studio>();
                // cfg.CreateMap<TriviaDto, Trivia>();
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
        }
    }
}
