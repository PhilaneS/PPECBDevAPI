
using API.Extensions;
using API.Filters;
using Application.Interfaces;
using Application.MapperProfile;
using Application.Services;
using Infrastructure.configuration;
using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.OperationFilter<FileUploadOperationFilter>();
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtService>();
            builder.Services.AddScoped<IProductCodeGenerator, ProdCodeGeneratorSevice>();
            builder.Services.AddScoped<IImageService, CloudinaryImageService>();
            builder.Services.AddScoped<IExcelService, ExcelService>();

            builder.Services.AddScoped<ProductService>();            
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<CategoryService>();            

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddCustomCors();

            builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
