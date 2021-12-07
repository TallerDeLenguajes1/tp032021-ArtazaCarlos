using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Threading.Tasks;
using tallerIIpractico3.Models.Db;
using NLog.Web;
using AutoMapper;


namespace tallerIIpractico3
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

            services.AddAutoMapper(typeof(sistemaDeCadeteria.PerfilDeMapeo));
            
            Logger logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();

            IRepositorioCadete RepositorioCadete = new RepositorioCadeteSQLite(Configuration.GetConnectionString("default"), logger);
            IRepositorioPedido RepositorioPedido = new RepositorioPedidoSQLite(Configuration.GetConnectionString("default"), logger);
            IRepositorioCliente RepositorioCliente = new RepositorioClienteSQLite(Configuration.GetConnectionString("default"), logger);
            IRepositorioUsuario RepositorioUsuario = new RepositorioUsuarioSQLite(Configuration.GetConnectionString("default"), logger);

            Db Db = new(RepositorioCadete, RepositorioPedido, RepositorioCliente, RepositorioUsuario);
            services.AddSingleton(Db);

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
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

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

          
        }
    }
}
