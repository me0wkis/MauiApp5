using CommunityToolkit.Mvvm.Input;
using MauiApp5.Models;

namespace MauiApp5.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}