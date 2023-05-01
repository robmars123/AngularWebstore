using DAL;
using DAL.Models;
using Infrastructure.EmailService;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ReactWebstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //add database data context connection
            builder.Services.AddDbContextPool<StoreDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("AngularWebstore")));

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200");
                });
            });
            //dependency injection
            builder.Services.AddScoped<StoreDBContext>();
            builder.Services.AddTransient<IRepository<Product>, ProductRepository>();
            //email service
            builder.Services.AddTransient<IEmailService<Email>, EmailServiceRepository>();

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}