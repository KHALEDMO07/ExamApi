
using Exam.Data;
using Exam.Middlewares;
using Exam.Models;
using Exam.Repositories;
using Exam.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Exam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64; // Optional: Increase max depth if necessary
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUserService , UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            //  builder.Services.Configure<Jwt>(builder.Configuration.GetSection("JWT")); 
            var JwtOptions = builder.Configuration.GetSection("JWT").Get<Jwt>();
            builder.Services.AddSingleton(JwtOptions);
            builder.Services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme , options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                       // ValidateIssuer = true , 
                        ValidIssuer = JwtOptions.Issuer , 
                       // ValidateAudience = true , 
                        ValidAudience = JwtOptions.Audience ,
                      //  ValidateLifetime = true , 
                      //  RequireExpirationTime = true, 
                       // ValidateIssuerSigningKey = true , 
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key)),
                        ClockSkew = new TimeSpan(0, 0, 5)
                    };
                    options.Events = new JwtBearerEvents
                    {
                        //OnChallenge = ctx => LogAttempt(ctx.Request.Headers, "OnChallenge"),
                        //OnTokenValidated = ctx => LogAttempt(ctx.Request.Headers, "OnTokenValidated")
                    };
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            
            app.UseHttpsRedirection();
            app.UseMiddleware<JwtBlacklistedMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            
        }
    }
}
