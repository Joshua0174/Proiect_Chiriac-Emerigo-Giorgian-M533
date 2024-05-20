
using DataAccessLayer.Repository;
using DataAccessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using api.Service;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("Default", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
             }
            });
            });
            //Repository
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //builder.Services.AddScoped(typeof(IUserRepo<>), typeof(UserRepo));
            //Services
            builder.Services.AddScoped(typeof(ITestService), typeof(TestService));
            builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
            builder.Services.AddScoped(typeof(IReviewService), typeof(ReviewService));
            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
            builder.Services.AddScoped(typeof(ICartService), typeof(CartService));
            builder.Services.AddScoped(typeof(ICartRepository), typeof(CartRepository));    

            //Database
            builder.Services.AddDbContext<MyDbContext>();
            builder.Services.AddIdentity<AppUser,IdentityRole>(options=>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true; 
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<MyDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
               options.DefaultChallengeScheme =
               options.DefaultForbidScheme =
               options.DefaultScheme =
               options.DefaultSignInScheme =
               options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                      System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigninKey"])
                   )
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
                
                options.AddPolicy("UserOnly", policy =>
                    policy.RequireRole("User"));
            });
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog();
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
