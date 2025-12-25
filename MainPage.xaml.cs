using System.Globalization;

namespace MauiApp5;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        ApplyLocalization();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ApplyLocalization(); // обновляем текст при входе на страницу
    }

    private void ApplyLocalization()
    {
        var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        if (lang == "ru")
        {
            Title = "Меню игры";
            TitleLabel.Text = "Крестики-нолики";
            PlayButton.Text = "Играть";
            ScoresButton.Text = "Рекорды";
            SettingsButton.Text = "Настройки";
        }
        else
        {
            Title = "Game menu";
            TitleLabel.Text = "Tic-Tac-Toe";
            PlayButton.Text = "Play";
            ScoresButton.Text = "Scores";
            SettingsButton.Text = "Settings";
        }
    }

    private async void OnPlayClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GamePage));
    }

    private async void OnScoresClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ScoresPage));
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }
}
