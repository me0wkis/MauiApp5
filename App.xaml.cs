// App.xaml.cs
using Microsoft.Maui.Storage;
using System.Globalization;
using System.Threading;

namespace MauiApp5;

public partial class App : Application
{
    public const string ThemePreferenceKey = "AppTheme";
    public const string LanguagePreferenceKey = "AppLanguage";

    public App()
    {
        InitializeComponent();

        // Читаем сохранённую тему
        var theme = Preferences.Get(ThemePreferenceKey, "system");
        ApplyTheme(theme);

        // Читаем сохранённый язык
        var lang = Preferences.Get(LanguagePreferenceKey, "ru");
        ApplyLanguage(lang);

        MainPage = new AppShell();
    }

    public static void ApplyTheme(string theme)
    {
        switch (theme)
        {
            case "light":
                Current.UserAppTheme = AppTheme.Light;
                break;
            case "dark":
                Current.UserAppTheme = AppTheme.Dark;
                break;
            default:
                Current.UserAppTheme = AppTheme.Unspecified;
                break;
        }
        Preferences.Set(ThemePreferenceKey, theme);
    }

    public static void ApplyLanguage(string languageCode)
    {
        var culture = new CultureInfo(languageCode);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        Preferences.Set(LanguagePreferenceKey, languageCode);
    }
}
