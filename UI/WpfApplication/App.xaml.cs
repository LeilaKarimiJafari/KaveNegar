using KarimiProject.ExcelManager;
using KarimiProject.Interfaces;
using KarimiProject.SqlService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
#nullable enable
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostcontext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IFileReader, ExcelReader>();
                services.AddSingleton<IDataService, SqlDataService>();

                services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(@"Server=.;Database=KarimiDataBase;User Id=sa;Password=Karimi@123"));

                services.AddStackExchangeRedisCache(option =>
                {
                    option.Configuration = "localhost:8083";// Configuration.GetConnectionString("LocalRedis");
                    option.InstanceName = "KarimiProject-";
                });

            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var statupForm = AppHost.Services.GetRequiredService<MainWindow>();
            statupForm.Show();

            base.OnStartup(e);
        }

        protected async override void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();

            base.OnExit(e);
        }
    }
}
