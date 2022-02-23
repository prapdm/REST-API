using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShopApi.Entities;
using ShopApi.Middleware;
using System;
using System.Text;
using ShopApi.Services;
using Microsoft.AspNetCore.Identity;
using ShopApi.Models;
using ShopApi.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using ShopApi.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace ShopApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }


        public void ConfigureServices(IServiceCollection services )
        {

            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CountryIsPoland", builder => builder.RequireClaim("Country", "Poland"));

            });
            services.AddDbContext<ShopDBContext>(options =>
            {
                if (_env.IsDevelopment() || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Docker")
                    options.UseSqlServer(Configuration.GetConnectionString("ApiDbConnection"));
                else
                    options.UseSqlServer(Environment.GetEnvironmentVariable("SQLAZURECONNSTR_ConnectionStrings"));
            });
            services.AddScoped<DBSeeder>();
            services.AddControllers().AddFluentValidation();
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddScoped<IAccountService, AccountService>();
           
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();

            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<PaginationQuery>, ShopQueryValidator>();
            services.AddScoped<IValidator<PaginationQuery>, ProductQueryValidator>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IValidator<CreateUpdateProductDto>, CreateUpdateProductDtoValidator>();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddControllers()
                 .AddXmlSerializerFormatters()
                 .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopApi", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>

                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(Configuration["AllowedOrigins"])

                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DBSeeder seeder)
        {
            app.UseResponseCaching();
            app.UseCors("FrontEndClient");
            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMiddleware<RequestTimeMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopAPI v1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
