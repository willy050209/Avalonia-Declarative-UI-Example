// filepath: Avalonia Declarative UI Example/Avalonia Declarative UI Example.Android/MainActivity.cs
using Android.App;
using Android.Content.PM;
using Avalonia.Android;

namespace Avalonia_Declarative_UI_Example.Android;

/// <summary>
/// Android 的主視窗 (Activity)。
/// </summary>
[Activity(
    Label = "Avalonia Declarative UI",
    Theme = "@style/Theme.AppCompat.Light.NoActionBar",
    Icon = "@android:drawable/sym_def_app_icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity
{
}
