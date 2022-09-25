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
using DutyBoard_Telegram.Commands.Callback.Admin;
using DutyBoard_Telegram.Services;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using DutyBoard_DataAccess.Account;
using DutyBoard_Models.Account;
using DutyBoard_Utility.Account.Email;
using Microsoft.AspNetCore.Identity;

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
            services.AddScoped<BaseCommand, ActivateTelegramUserCommand>();
            services.AddScoped<BaseCommand, ActivateSiteUserCommand>();
            services.AddScoped<ITelegramUserService, TelegramUserService>();
            services.AddSingleton<TelegramBot>();
            services.AddScoped<RequestActivationCommand>();

            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();
        }

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
