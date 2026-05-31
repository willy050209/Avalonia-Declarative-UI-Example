// filepath: Avalonia Declarative UI Example/Avalonia Declarative UI Example.Shared/Services/GreetingService.cs
using System;

namespace Avalonia_Declarative_UI_Example.Shared.Services;

/// <summary>
/// 招呼語服務，負責提供應用程式所需的文字資料。
/// 遵循單一職責原則 (SRP)，將資料來源與 UI 展示邏輯分離，使程式更容易維護與測試。
/// </summary>
public sealed class GreetingService
{
    /// <summary>
    /// 取得新的招呼語文字。
    /// </summary>
    /// <returns>招呼語字串</returns>
    public string GetGreetingMessage()
    {
        // 這裡在真實專案中可以擴充為從 API、本地資料庫或設定檔讀取資料。
        return "Hello from Avalonia C# Markup & MVVM!";
    }

    /// <summary>
    /// 模擬一個高負載的任務，例如從網路 API 取得資料或進行複雜計算。
    /// </summary>
    public static Task HighLoadTasks()
    {
        // 模擬高負載任務
        return Task.Delay(1000);
    }
}
