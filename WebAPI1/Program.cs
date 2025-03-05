
using Microsoft.EntityFrameworkCore;
using WebAPI1.Models;

namespace WebAPI1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ITIContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            //cors Servise built in service need to register
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("myPolicy", policy => {

                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();//for test
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();//folder wwwroote

            app.UseCors("myPolicy");  //allow cross Domin

            //default routing controller
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
