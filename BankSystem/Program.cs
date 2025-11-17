using BLL.Services;
using Core;
using DAL;
using Microsoft.Extensions.DependencyInjection;

namespace BankSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<MainForm>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<BankDbContext>();
            var provider = services.BuildServiceProvider();

            Application.Run(provider.GetRequiredService<MainForm>());
        }
    }
}