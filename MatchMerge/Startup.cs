using Blazored.LocalStorage;
using Blazored.SessionStorage;
using MatchMerge.Data;
using MatchMerge.Data.Universe.Activities;
using MatchMerge.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchMerge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSyncfusionBlazor();
            services.AddScoped<IDapperService, DapperService>();
            services.AddScoped<IIndividuViewService, IndividuViewService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<ISpecialiteService, SpecialiteService>();

            services.AddScoped<DialogService>();
            services.AddScoped<IIndividuService, IndividuService>();
            services.AddScoped<IActiviteService, ActiviteService>();
            services.AddScoped<IEtablissementService, EtablissementService>();
            services.AddScoped<IPaysService, PaysService>();
            services.AddScoped<IDepartementService, DepartementService>();
            services.AddScoped<ITypeIndividuService, TypeIndividuService>();
            //services.AddScoped<ICiviliteService, CiviliteService>();
            //services.AddScoped<ITitreService, TitreService>();
            services.AddScoped<ICategorieEtablissementService, CategorieEtablissementService>();
            services.AddScoped<ITypeEtablissementService, TypeEtablissementService>();
            services.AddScoped<ISpecialiteService, SpecialiteService>();
            services.AddScoped<IUniversService, UniverService>();
            services.AddScoped<IUtilisateurService, UtilisateurService>();
            services.AddScoped<IDecoupageService, DecoupageService>();
            services.AddScoped<IDecoupageDetailService, DecoupageDetailService>();     
            services.AddScoped<IEtablissementTerritoireService, EtablissementTerritoireService>();
            services.AddScoped<ExportExcelService>();
            services.AddBlazoredSessionStorage();
            services.AddBlazoredLocalStorage();
            services.AddScoped<TooltipService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTUzNzM0QDMxMzkyZTM0MmUzME5keFlBbUxwN1lkYlgxdWcxbnFheTkyRkpRbkRreGxsRFhUUTBTS2E3THM9");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
