using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApiClient.Data;
using WebApiClient.Interfaces;
using WebApiClient.Models;
using WebApiClient.Services;

namespace WebApiClient
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
            services.AddControllersWithViews();
            services.AddDbContext<PersonContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IContextService, ContextService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.RequireHttpsMetadata = false;
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         // ��������, ����� �� �������������� �������� ��� ��������� ������
                         ValidateIssuer = true,
                         // ������, �������������� ��������
                         ValidIssuer = AuthOptions.ISSUER,

                         // ����� �� �������������� ����������� ������
                         ValidateAudience = true,
                         // ��������� ����������� ������
                         ValidAudience = AuthOptions.AUDIENCE,
                         // ����� �� �������������� ����� �������������
                         ValidateLifetime = true,

                         // ��������� ����� ������������
                         IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                         // ��������� ����� ������������
                         ValidateIssuerSigningKey = true,
                     };
                 });
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/");
            });
        }
    }
}
