using CommunityToolkit.Mvvm.DependencyInjection;
using EFTest.ViewModels;
using System.Windows;

namespace EFTest.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<MainVM>();
        }
    }
}
