using DutyBoard_DataAccess.Repository;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Telegram.Interface;
using DutyBoard_Telegram;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DutyBoard_Telegram.Commands;
using DutyBoard_Telegram.Commands.Callback.Users;
using DutyBoard_Telegram.Services;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DutyBoard
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
            services.AddScoped<IDaysOfWeekRepository, DaysOfWeekRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<IWorkdayRepository, WorkdayRepository>();
            services.AddScoped<IMappingRepository, MappingRepository>();
            services.AddScoped<IRosterRepository, RosterRepository>();
            services.AddScoped<IExportRepository, ExportRepository>();
            services.AddScoped<ISiteUserRepository, SiteUserRepository>();
            services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();


            //Telegram
            services.AddScoped<ICommandExecutor, CommandExecutor>();
            services.AddScoped<BaseCommand, StartCommand>();
            services.AddScoped<BaseCommand, AdminCommand>();
            services.AddScoped<BaseCommand, UsersCommand>();
            services.AddScoped<BaseCommand, WhoDutyCommand>();
            services.AddScoped<BaseCommand, ListDutyCommand>();
            services.AddScoped<BaseCommand, FileCommand>();
            services.AddScoped<BaseCommand, HelpCommand>();
            services.AddScoped<BaseCommand, GetAnalyticCommand>();
            services.AddScoped<ITelegramUserService, TelegramUserService>();
            services.AddScoped<TelegramBot>();

            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            WC.Path = env.ContentRootPath;
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

            app.UseAuthentication();
            app.UseAuthorization();

            serviceProvider.GetRequiredService<TelegramBot>().GetBot().Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
