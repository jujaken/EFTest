using CommunityToolkit.Mvvm.DependencyInjection;
using EFTest.Data;
using EFTest.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EFTest
{

    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(GetServices());
        }

        private IServiceProvider GetServices()
        {
            var serviceCollection = new ServiceCollection()
                .AddDbContext<AppDbContext>()
                .AddTransient<IRepo<VectorModel>, Repo<VectorModel>>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
