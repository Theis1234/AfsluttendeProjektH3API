
using AfsluttendeProjektH3API;
using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Application.Services;
using AfsluttendeProjektH3API.Infrastructure;
using AfsluttendeProjektH3API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AfsluttendeProjektH3API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", 
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<ICoverRepository, CoverRepository>();
            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped<ArtistService>();
            builder.Services.AddScoped<BookService>();
            builder.Services.AddScoped<CoverService>();

            var app = builder.Build();

            app.UseCors("AllowAngularApp");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
