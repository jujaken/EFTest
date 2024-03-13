using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using EFTest.Data;
using EFTest.Models;

namespace EFTest.ViewModels
{
    public partial class VectorVM(VectorModel vector) : ObservableObject
    {
        private readonly VectorModel vector = vector;
        private readonly IRepo<VectorModel> repo = Ioc.Default.GetRequiredService<IRepo<VectorModel>>();

        [ObservableProperty]
        private double x;

        [ObservableProperty]
        private double y;

        [ObservableProperty]
        private double z;

        [RelayCommand]
        public async Task SaveCoordinates()
        {
            vector.Coordinates = [X, Y, Z];
            await repo.Update(vector);
        }
    }
}
