// filepath: Avalonia Declarative UI Example/Avalonia Declarative UI Example.Shared/Config.cs
namespace Avalonia_Declarative_UI_Example.Shared;

/// <summary>
/// 應用程式全域靜態組態配置。
/// 集中管理系統層級的唯讀常數與設定，避免硬編碼字串散落於各平台專案中。
/// 使用 Get-only 屬性確保這些配置在運行時不會被修改，增強程式的穩定性與可維護性。
/// </summary>
public static class Config
{
    // 視窗與應用程式標題
    public static string WindowsTitle { get; } = "Avalonia Declarative Template (Desktop)";

    // 應用程式版本號
    public static string Version { get; } = "1.0.0";

    // 視窗寬度
    public static int WindowWidth { get; } = 800;

    // 視窗高度
    public static int WindowHeight { get; } = 600;
}
