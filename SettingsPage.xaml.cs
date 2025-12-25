using System.Globalization;
using Microsoft.Maui.Storage;

namespace MauiApp5;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        InitTheme();
        InitLanguage();
        ApplyLocalization();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ApplyLocalization();
    }

    private void ApplyLocalization()
    {
        var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        if (lang == "ru")
        {
            Title = "Настройки";
            ThemeLabel.Text = "Тема";
            LanguageLabel.Text = "Язык";
        }
        else
        {
            Title = "Settings";
            ThemeLabel.Text = "Theme";
            LanguageLabel.Text = "Language";
        }
    }

    // InitTheme / InitLanguage как раньше

    private void OnThemeChanged(object sender, EventArgs e)
    {
        var index = ThemePicker.SelectedIndex;
        var theme = index switch
        {
            1 => "light",
            2 => "dark",
            _ => "system"
        };
        App.ApplyTheme(theme);
    }

    private void OnLanguageChanged(object sender, EventArgs e)
    {
        var index = LanguagePicker.SelectedIndex;
        var lang = index == 1 ? "en" : "ru";

        App.ApplyLanguage(lang);
        ApplyLocalization();          // обновляем текущую страницу

        // остальные страницы обновятся при следующем открытии
    }
}
