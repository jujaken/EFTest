using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFTest.Data;
using EFTest.Models;

namespace EFTest.ViewModels
{
    public partial class VectorVM(IRepo<VectorModel> repo) : ObservableObject
    {
        private readonly IRepo<VectorModel> repo = repo;
        private VectorModel? curVector;

        [ObservableProperty]
        private double x;

        [ObservableProperty]
        private double y;

        [ObservableProperty]
        private double z;

        [ObservableProperty]
        private bool isSelected = false;

        [RelayCommand]
        public async Task SetVector(VectorModel vector)
        {
            if (vector.Coordinates == null)
            {
                vector.Coordinates = [0, 0, 0];
                await repo.Update(vector);
            }
            else if (vector.Coordinates.Length < 3)
            {
                var coordinates = new double[3];

                for (var i = 0; i < vector.Coordinates.Length; i++)
                    coordinates[i] = vector.Coordinates[i];

                vector.Coordinates = coordinates;
                await repo.Update(vector);
            }

            curVector = vector;
            (X, Y, Z) = (vector.Coordinates[0], vector.Coordinates[1], vector.Coordinates[2]);
        }

        [RelayCommand]
        public async Task CreateNewVector()
        {
            var coordinates = new double[] { X, Y, Z };

            if (curVector?.Coordinates?.Zip(coordinates).Count() == 0)
                return;

            curVector = new VectorModel() { Coordinates = coordinates };
            await repo.Create(curVector);
        }

        public bool CanSaveVectorChanges => curVector != null;

        [RelayCommand(CanExecute = nameof(CanSaveVectorChanges))]
        public async Task SaveVectorChanges()
        {
            curVector!.Coordinates = [X, Y, Z];
            await repo.Update(curVector);
        }
    }
}
