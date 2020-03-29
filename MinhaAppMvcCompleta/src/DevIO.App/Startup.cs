using AutoMapper;
using DevIO.App.Configurations;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevIO.App
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


            //adicionado o IdentityConfig passando o Configuration
            services.AddIdentityConfiguration(Configuration);


            services.AddDbContext<MeuDbContext>(options =>
              options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection")));


            services.AddControllersWithViews();
            services.AddRazorPages();

            //adicionado para permitir que as p�ginas html possam ser alteradas 
            //https://stackoverflow.com/questions/54600273/net-core-3-0-preview-2-razor-views-dont-automatically-recompile-on-change
            services.AddControllersWithViews().AddRazorRuntimeCompilation();


            //configurando o AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //valida��es usando o MvcConfig
            services.AddMvcConfiguration();

            //configurando o refer�ncia do IRepository com o Data Repository (Inje��o de depend�ncia)
            services.ResolveDependencies();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            //usar a globaliza��o
            app.UseGlobalizationConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
