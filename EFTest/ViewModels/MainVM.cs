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
        private int curVectorVMIndex;

        public VectorVM? CurVectorVM
        {
            get
            {
                if (VectorsVMs.Count() == 0)
                    return null;

                return VectorsVMs[CurVectorVMIndex];
            }
        }

        [RelayCommand]
        public async Task CreateVectors()
        {
            VectorsVMs = [];
            for (int i = 0; i < 10; i++)
            {
                var vectorVm = services.GetRequiredService<VectorVM>();
                await vectorVm.CreateNewVector();
                VectorsVMs.Add(vectorVm);
            }
            CurVectorVMIndex = 0;
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
            CurVectorVMIndex = 0;
        }

        public bool CanDownVector => CurVectorVMIndex != 0;

        [RelayCommand(CanExecute = nameof(CanDownVector))]
        public void DownVector()
        {
            CurVectorVMIndex--;
        }

        public bool CanUpVector => CurVectorVMIndex != VectorsVMs.Count - 1;

        [RelayCommand(CanExecute = nameof(CanUpVector))]
        public void UpVector()
        {
            CurVectorVMIndex++;
        }
    }
}
