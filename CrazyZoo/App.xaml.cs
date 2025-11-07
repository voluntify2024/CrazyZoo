using CrazyZoo.Animals;
using CrazyZoo.Interfaces;
using CrazyZoo.Repositories;
using CrazyZoo.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CrazyZoo
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddSingleton<IRepository<Animal>>(provider =>
                new AnimalRepository(@"Server=(localdb)\MSSQLLocalDB;Database=CrazyZooDB;Trusted_Connection=True;"));

            services.AddSingleton<EnclosureManager>();

            services.AddSingleton<ILogger, JsonLogger>();

            services.AddTransient<MainWindow>();
            services.AddTransient<AddAnimalWindow>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
