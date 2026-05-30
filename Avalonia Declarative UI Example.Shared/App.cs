// filepath: Avalonia Declarative UI Example/Avalonia Declarative UI Example.Shared/App.cs
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Declarative;
using Avalonia_Declarative_UI_Example.Shared.Views;


namespace Avalonia_Declarative_UI_Example.Shared;

/// <summary>
/// 共享應用程式類別 (Application)。
/// 這個類別在 Desktop 與 Android 平台上都是共享的。
/// 它負責在應用程式初始化完成後，根據當前的平台生命週期 (Lifetime) 建立對應的 UI。
/// </summary>
public partial class App : Application
{
    public override void Initialize()
    {
        Styles.Add(new FluentTheme());
    }

    /// <summary>
    /// 當 Avalonia 框架初始化完成時觸發。
    /// 這是跨平台 UI 設計的核心：在此處判斷我們是運行於桌面端（擁有視窗）還是行動端（單一檢視）。
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // ====================================================
            // 桌面端 (Windows / macOS / Linux) 生命週期設定
            // ====================================================
            // 在桌面端，我們需要建立一個視窗 (Window)，並將 MainView 設為該視窗的內容。
            desktop.MainWindow = new Window()
                .Title($"{Config.WindowsTitle} v{Config.Version}")
                .Width(Config.WindowWidth)
                .Height(Config.WindowHeight)
                .Content(ViewFactory.Create<MainView>());
        }
        else if (ApplicationLifetime is IActivityApplicationLifetime activityLifetime)
        {
            // ====================================================
            // Android 平台生命週期設定 (Avalonia v12+)
            // ====================================================
            // 在 Android 上，因為 Activity 可能因為系統原因被銷毀並重新建立，
            // 我們不能使用單一的靜態控制項，否則會發生「控制項已有父控制項」的異常。
            // 因此，我們需要將 MainViewFactory 設為一個委派函數，每次 Android 需要時就建立一個新的 MainView 實例。
            activityLifetime.MainViewFactory = () => ViewFactory.Create<MainView>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
