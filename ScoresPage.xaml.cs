using System.Globalization;
using MauiApp5.Services;

namespace MauiApp5;

public partial class ScoresPage : ContentPage
{
    // ...

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ApplyLocalization();
        await LoadScoresAsync();
    }

    private void ApplyLocalization()
    {
        var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        if (lang == "ru")
        {
            Title = "Рекорды";
            LocalButton.Text = "Локальные";
            GlobalButton.Text = "Глобальные";
            SyncButton.Text = "Синхронизировать";
        }
        else
        {
            Title = "Scores";
            LocalButton.Text = "Local";
            GlobalButton.Text = "Global";
            SyncButton.Text = "Sync → Global";
        }
    }

    // остальной код без изменений
}
