using DB.Mongo.Serializers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using siberianoilrush_server.Auth;
using siberianoilrush_server.Config;
using siberianoilrush_server.Database.Mongo.Database;
using siberianoilrush_server.Database.Mongo.Repository;
using siberianoilrush_server.Database.Mongo.Repository.Sor;
using siberianoilrush_server.Hosted;
using siberianoilrush_server.Service;
using siberianoilrush_server.Service.Inventory;
using siberianoilrush_server.Service.Public;
using System;

namespace siberianoilrush_server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(typeof(DateTime), new BsonUtcDateTimeSerializer());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(jsOption => { jsOption.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto; });

            services.AddLogging();

            services.Configure<Section>(Configuration);

            services.AddTransient<InventoryDb>();

            #region MongoDb
            services.AddSingleton<IVersionHistoryDB, VersionHistoryDB>();
            services.AddSingleton<ILogDB, LogDB>();
            #endregion

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient(typeof(MongoEssence<>));

            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<IVersionService, VersionService>();

            #region service
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserOldService, UserOldService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IExtractionService, ExtractionService>();
            services.AddTransient<ICaseService, CaseService>();
            services.AddTransient<IAPService>();
            
            services.AddTransient<IReadWalletService, WalletService>();
            services.AddTransient<IWriteWalletService, WalletService>();
            #endregion

            services.AddHostedService<BuildingManagementHostedService>();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            ILoggerService loggerService)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();

            loggerFactory.AddProvider(new LoggerProvider(loggerService));
            var logger = loggerFactory.CreateLogger("Logger");
        }
    }
}
