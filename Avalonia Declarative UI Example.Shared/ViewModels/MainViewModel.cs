// filepath: Avalonia Declarative UI Example/Avalonia Declarative UI Example.Shared/ViewModels/MainViewModel.cs
using System;
using Avalonia_Declarative_UI_Example.Shared.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia_Declarative_UI_Example.ViewModels;

/// <summary>
/// 主畫面的檢視模型 (ViewModel)。
/// 繼承自 CommunityToolkit.Mvvm 的 ObservableObject，提供屬性變更通知功能。
/// 這是 MVVM 架構的核心：所有 UI 狀態與使用者操作邏輯都寫在這裡，與具體的 UI 框架無關，利於單元測試。
/// </summary>
public partial class MainViewModel : ObservableObject
{
    // 依賴注入的服務物件，宣告為唯讀 (readonly) 以免被意外修改
    private readonly GreetingService _greetingService;

    /// <summary>
    /// 招呼語文字屬性。
    /// 使用 [ObservableProperty] 標記後，Mvvm Toolkit 的 Source Generator 會自動在背景產生一個名為 "GreetingText" 的公開屬性，
    /// 並且在值改變時自動發出 PropertyChanged 事件，通知 UI 更新。
    /// 註：底線開頭的欄位 _greetingText 會自動映射為大寫駝峰的公開屬性 GreetingText。
    /// </summary>
    [ObservableProperty]
    private string _greetingText = "Hello World!";

    /// <summary>
    /// 按鈕被點擊的次數計數器。
    /// </summary>
    [ObservableProperty]
    private int _clickCount = 0;

    /// <summary>
    /// 系統是否正處於處理狀態（例如正在載入外部資料）。
    /// </summary>
    [ObservableProperty]
    private bool _isBusy;

    /// <summary>
    /// 主要建構函式，利用 .NET 內建的相依性注入 (Dependency Injection)。
    /// 這裡注入了 GreetingService，以符合依賴反轉原則 (DIP) 與職責分離。
    /// </summary>
    /// <param name="greetingService">招呼語服務實例，由 DI 容器自動傳入</param>
    public MainViewModel(GreetingService greetingService)
    {
        // 防禦性編程：確保注入的服務不為 null，若為 null 則立即拋出異常 (Fail-Fast)
        ArgumentNullException.ThrowIfNull(greetingService);
        _greetingService = greetingService;
    }

    /// <summary>
    /// 更新招呼語與計數的指令。
    /// 當使用者在 UI 點擊按鈕時，會透過事件處理器呼叫此方法。
    /// </summary>
    [RelayCommand]
    public void UpdateGreeting()
    {
        // 1. 遞增點擊計數
        ClickCount++;

        // 2. 結合服務取得的基本問候語與累加計數，更新 GreetingText。
        // 當此屬性更新時，UI 上綁定 GreetingText 的 TextBlock 就會同步且自動重繪更新！
        GreetingText = $"{_greetingService.GetGreetingMessage()}\n按鈕已被點擊 {ClickCount} 次！";
    }

    /// <summary>
    /// 模擬執行一個高負載的任務，例如從網路載入資料或進行複雜計算。
    /// </summary>
    [RelayCommand]
    public async Task DoHighLoadTasks()
    {
        IsBusy = true; // 開始處理，設定忙碌狀態
        GreetingText = "正在執行高負載任務..."; // 更新招呼語以反映正在處理的狀態
        try
        {
            await GreetingService.HighLoadTasks(); // 執行模擬高負載任務
            GreetingText = "高負載任務完成！"; // 更新招呼語以反映任務完成
        }
        finally
        {
            IsBusy = false; // 無論成功與否，都結束忙碌狀態
        }
    }
}
