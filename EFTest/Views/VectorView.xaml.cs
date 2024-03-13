using CommunityToolkit.Mvvm.DependencyInjection;
using EFTest.ViewModels;
using System.Windows.Controls;

namespace EFTest.Views
{
    public partial class VectorView : UserControl
    {
        public VectorView()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<VectorVM>();
        }
    }
}
