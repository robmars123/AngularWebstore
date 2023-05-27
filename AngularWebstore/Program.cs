using DAL;
using DAL.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.EmailService;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Repositories.Products;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
            builder.Services.AddTransient<StoreDBContext>();
            builder.Services.AddTransient<IRepository<Product>, ProductRepository>();
            builder.Services.AddTransient<IProductImage<ProductImage>, ProductImageRepository>();
            //email service
            builder.Services.AddTransient<IEmailService<Email>, EmailServiceRepository>();

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set 
                                                                  //default value is: 30 MB
            });
            builder.Services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue; // if don't set 
                                                                 //default value is: 128 MB
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

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