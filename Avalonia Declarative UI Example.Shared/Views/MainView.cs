// filepath: Avalonia Declarative UI Example/Avalonia Declarative UI Example.Shared/Views/MainView.cs
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia_Declarative_UI_Example.ViewModels;

namespace Avalonia_Declarative_UI_Example.Shared.Views;

/// <summary>
/// 主畫面檢視 (View)。
/// 繼承自 ViewBase&lt;MainViewModel&gt;，這是 Avalonia Declarative UI 提供的一種強型別檢視基底類別。
/// 透過將 MainViewModel 傳入，我們可以利用強型別的資料繫結 (Data Binding) 來建立 UI。
/// </summary>
public class MainView(MainViewModel viewModel) : ViewBase<MainViewModel>(viewModel)
{
    /// <summary>
    /// 建立控制項的樣式群組 (Styles)。
    /// 宣告式 UI 可以直接在 C# 中定義重複使用的樣式，避免在個別控制項上重複設定相同的屬性。
    /// 這非常類似網頁開發中的 CSS 類別或全域樣式設定。
    /// </summary>
    protected override StyleGroup? BuildStyles() =>
    [
        // 定義全域按鈕 (Button) 的基本樣式
        new Style<Button>()
            .Background(Brushes.RoyalBlue)
            .Foreground(Brushes.White)
            .Padding(new Thickness(24, 12))
            .CornerRadius(new CornerRadius(8))
            .FontSize(16)
            .FontWeight(FontWeight.Medium),

        // 定義 Button 在滑鼠懸停 (:pointerover) 時的樣式
        new Style<Button>(x => x.OfType<Button>().Class(":pointerover").Template().OfType<ContentPresenter>())
            .Background(Brushes.DodgerBlue)
            .Foreground(Brushes.White)
    ];

    /// <summary>
    /// 建構 UI 樹狀結構。
    /// 在此方法中，我們使用 C# 宣告式語法來配置 UI 排版與控制項。
    /// </summary>
    /// <param name="vm">繫結的 ViewModel 實例 (等同於 MVVM 中的 DataContext)</param>
    /// <returns>UI 樹狀結構的根節點</returns>
    protected override object Build(MainViewModel vm) =>
        new Grid()
            .Background(Brushes.WhiteSmoke) // 設定整個畫面的背景顏色為淡灰色
            .Children(
                new StackPanel()
                    .VerticalAlignment(VerticalAlignment.Center)   // 垂直置中
                    .HorizontalAlignment(HorizontalAlignment.Center) // 水平置中
                    .Spacing(24) // 設定子控制項之間的間距 (24 像素)
                    .Children(
                        // ====================================================
                        // 1. 文字方塊 (TextBlock) - 用於顯示文字內容
                        // ====================================================
                        new TextBlock()
                            // 資料繫結 (Data Binding)：將 Text 屬性綁定到 ViewModel 的 GreetingText 屬性。
                            // 當 vm.GreetingText 改變時，這個 TextBlock 就會自動更新內容！
                            .Text(vm, x => x.GreetingText) 
                            .FontSize(28) // 字型大小
                            .FontWeight(FontWeight.Bold) // 粗體字
                            .Foreground(Brushes.DarkSlateBlue) // 深石板藍字體顏色
                            .HorizontalAlignment(HorizontalAlignment.Center),

                        // ====================================================
                        // 2. 按鈕 (Button) - 點擊後執行動作
                        // ====================================================
                        new Button()
                            .Content("點擊更新文字 (Click Me)")
                            .HorizontalAlignment(HorizontalAlignment.Center)
                            // 動作繫結：當按鈕被點擊 (OnClick) 時，執行 ViewModel 內定義的 UpdateGreeting 方法
                            .OnClick(_ => vm.UpdateGreeting())
                    )
            );
}
