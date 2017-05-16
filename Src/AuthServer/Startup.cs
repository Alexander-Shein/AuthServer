using AuthGuard.BLL.Domain.Entities;
using AuthGuard.Data;
using AuthGuard.SL.Apps;
using AuthGuard.SL.Notifications;
using DddCore.Contracts.DAL;
using DddCore.Contracts.DAL.DomainStack;
using DddCore.Crosscutting.DependencyInjection;
using DddCore.Crosscutting.ObjectMapper;
using DddCore.Crosscutting.ObjectMapper.AutoMapper;
using DddCore.DAL.DomainStack.EntityFramework.Context;
using DddCore.SL.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthGuard
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
                {
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            services.AddScoped<IUnitOfWork>(x => x.GetService<ApplicationDbContext>());
            services.AddScoped<IDataContext>(x => x.GetService<ApplicationDbContext>());

            var module = new DddCoreDiModuleInstaller();

            new DiBootstrapper()
                .AddConfig(services)
                .Bootstrap(module);
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            var mapper = new ObjectMapperBootstrapper()
                .AddAutoMapperConfig()
                .Bootstrap();

            services.AddSingleton(mapper);

            services.AddMvc();
            //services.AddDddCore();

            // Add application services.
            services.AddTransient<IEmailSender, NotificationsService>();
            services.AddTransient<ISmsSender, NotificationsService>();
            //services.AddScoped<IClientStore, ClientsStore>();
            services.AddScoped<IRedirectUriValidator, AppRedirectUrlValidator>();
            //services.AddScoped<ITokensService, TokensService>();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddConfigurationStore(builder =>
                    builder.UseSqlServer("Server=(local);Database=IdentityServer4.Quickstart.AspNetIdentity;Trusted_Connection=True;MultipleActiveResultSets=true"))
                .AddOperationalStore(builder =>
                    builder.UseSqlServer("Server=(local);Database=IdentityServer4.Quickstart.AspNetIdentity;Trusted_Connection=True;MultipleActiveResultSets=true"))
                .AddAspNetIdentity<ApplicationUser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseIdentity();
            app.UseIdentityServer();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                SignInScheme = "Identity.External", // this is the name of the cookie middleware registered by UseIdentity()
                ClientId = "998042782978-s07498t8i8jas7npj4crve1skpromf37.apps.googleusercontent.com",
                ClientSecret = "HsnwJri_53zn7VcO1Fm7THBb"
            });

            app.UseCors("default");

            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute("default", "~/");
            });
        }
    }
}