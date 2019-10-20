using System.Threading.Tasks;
using UnoMvvm;

namespace UnoWebApiSwagger.ViewModels
{
    public class LoadViewModel : BindableBase, IUnloadViewModel, IViewModel
    {
        private bool _isBusy; public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }
        public virtual Task Load() => Task.CompletedTask;
        public virtual void Unload() { }
    }
}