using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFTest.Data;
using EFTest.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace EFTest.ViewModels
{
    public partial class MainVM(IServiceProvider services, IRepo<VectorModel> repo) : ObservableObject
    {
        private readonly IServiceProvider services = services;
        private readonly IRepo<VectorModel> repo = repo;

        [ObservableProperty]
        private ObservableCollection<VectorVM> vectorsVMs = [];

        [ObservableProperty]
        private VectorVM? curVectorVM;

        [RelayCommand]
        public async Task CreateVectors()
        {
            VectorsVMs = [];
            for(int i = 0; i < 10; i++)
            {
                var vectorVm = services.GetRequiredService<VectorVM>();
                await vectorVm.CreateNewVector();
                VectorsVMs.Add(vectorVm);
            }
        }

        [RelayCommand]
        public async Task LoadVectors()
        {
            VectorsVMs = [];
            (await repo.GetAll()).ForEach(async v =>
            {
                var vectorVm = services.GetRequiredService<VectorVM>();
                await vectorVm.SetVector(v);
                VectorsVMs.Add(vectorVm);
            });
        }
    }
}
