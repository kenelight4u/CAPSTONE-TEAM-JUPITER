using JupiterCapstone.Data;
using JupiterCapstone.Models;
using JupiterCapstone.Profiles;
using JupiterCapstone.SendGrid;
using JupiterCapstone.Services;
using JupiterCapstone.Services.AuthorizationServices;
using JupiterCapstone.Services.GoogleServices;
using JupiterCapstone.Services.GoogleServices.IGoogleService;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JupiterCapstone
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
            //configure the SQL connection string for migration but I commented it out because I changed my Db to postgres

            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration["ConnectionStrings:ApplicationDbConnection"]);
            //});

            //configure the Postgres connection string for migration
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql
                    (Configuration.GetConnectionString("PostgreSqlConnection")));

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("TokenConfiguration");
            services.Configure<TokenConfiguration>(appSettingsSection);

            // configure strongly typed settings objects sms
            var appSmsSetting = Configuration.GetSection("SmsConfiguration");
            services.Configure<SmsConfiguration>(appSmsSetting);

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IShippingAddressService, ShippingAddressService>();
            services.AddScoped<IGoogleIdentity, GoogleIdentity>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategory, CategoryAccess>();
            services.AddScoped<ISubCategory, SubCategoryAccess>();
            services.AddScoped<IProduct, ProductAccess>();
            services.AddScoped<IShoppingCartActions, ShoppingCartActions>();
            services.AddScoped<IOrder, OrderAccess>();
            services.AddScoped<IPayment, PaymentAccess>();
            services.AddScoped<IWishListActions, WishListActions>();
            services.AddScoped<IImageService, ImageUpload>();

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //registering Send Grid 
            services.AddTransient<IMailService, SendGridMailService>();
           
             //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(UsersProfile));
            services.AddAutoMapper(typeof(ShippingAddressProfile));

            // For Identity  
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // configure jwt authentication
            var serviceConfiguration = appSettingsSection.Get<TokenConfiguration>();
            var JwtSecretkey = Encoding.ASCII.GetBytes(serviceConfiguration.JwtSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
                ValidIssuer = Configuration["TokenConfiguration:JwtSettings:ValidIssuer"],
                ValidAudience= Configuration["TokenConfiguration:JwtSettings:ValidAudience"],
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;

            });

            //password tightening up
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8; 
            });

            //for httpcontext
            services.AddHttpContextAccessor();

            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Aduaba",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ADUABA API V1");
                //c.RoutePrefix = string.Empty;
            });

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
