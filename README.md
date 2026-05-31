# Avalonia Declarative UI & MVVM 跨平台開發範本

這是一個專門為了引導開發者快速上手 **Avalonia UI** 跨平台開發所設計的模板專案。
本專案採用 **宣告式 UI (C# Markup)** 技術，完全不使用複雜的 XML (AXAML)，並導入 **MVVM 架構** 與 **相依性注入 (Dependency Injection)**，幫助初學者在起步時就建立良好的軟體工程習慣。

---

## 🚀 專案特點

1. **零 XML / AXAML**：所有的 UI 畫面都使用純 C# 撰寫，享有完整的 IDE 自動完成、型別安全檢查與輕鬆的重構支援。
2. **跨平台支援**：使用同一套 UI 與邏輯程式碼，同時支援 **Desktop (Windows/macOS/Linux)** 與 **Android** 平台。
3. **標準 MVVM 架構**：
   * **Model/Service**：獨立的商業邏輯與資料層。
   * **ViewModel**：使用微軟官方推薦的 `CommunityToolkit.Mvvm` 進行屬性更新通知。
   * **View**：由純 C# 宣告控制項，並透過強型別進行資料繫結 (Data Binding)。
4. **相依性注入 (DI)**：整合 `Microsoft.Extensions.DependencyInjection`，確保元件之間維持低耦合。

---

## 📁 專案目錄結構

為落實職責分離，專案共拆分為以下三個主要專案：

```text
├── Avalonia Declarative UI Example.slnx    # Visual Studio 2022+ 新版解決方案檔
│
├── 📂 Avalonia Declarative UI Example.Shared      # 【核心共用專案】
│   ├── App.cs                                     # 應用程式初始化與跨平台 UI 生命週期配置
│   ├── Config.cs                                  # 全域靜態組態配置（視窗大小、標題、版本等）
│   ├── GlobalUsings.cs                            # 共享專案全域引用配置
│   ├── 📂 Services                                # 【服務層 / Model】
│   │   └── GreetingService.cs                     # 提供展示資料的服務實例
│   ├── 📂 ViewModels                              # 【檢視模型層】
│   │   └── MainViewModel.cs                       # 控制 UI 狀態與執行動作指令 (Command)
│   └── 📂 Views                                   # 【檢視層】
│       └── MainView.cs                            # 純 C# Markup 宣告的 UI 元件
│
├── 📂 Avalonia Declarative UI Example.Desktop     # 【Desktop 執行專案】
│   ├── Program.cs                                 # 桌面端程式載入點
│   ├── GlobalUsings.cs                            # 桌面專案全域引用配置
│   └── ...
│
└── 📂 Avalonia Declarative UI Example.Android     # 【Android 執行專案】
    ├── MainActivity.cs                            # Android 主畫面 Activity
    ├── MainApplication.cs                         # Android App 初始化與 DI 註冊
    ├── GlobalUsings.cs                            # Android 專案全域引用配置
    └── AndroidManifest.xml                        # Android 清單設定檔
```

---

## 🛠️ 如何編譯與執行

在開始之前，請確保您的開發環境已安裝 [.NET 10 SDK](https://dotnet.microsoft.com/)，以及 Android 的開發工具。

### 💻 執行桌面版 (Desktop)
在終端機中切換至專案根目錄，並執行以下指令：
```bash
dotnet run --project "Avalonia Declarative UI Example.Desktop"
```

### 📱 執行行動版 (Android)
確認已連接實體 Android 裝置或啟動模擬器後，執行以下指令：
```bash
dotnet run --project "Avalonia Declarative UI Example.Android"
```
*(注意：首次建置 Android 可能需要幾分鐘以處理 SDK 還原。)*

---

## 📖 核心概念與寫法引導

本範本中每個核心檔案皆有詳盡註解，以下是引導學弟開發時的幾個關鍵核心：

### 1. 什麼是 C# 宣告式 UI (C# Markup)？
在 [Views/MainView.cs](./Avalonia%20Declarative%20UI%20Example.Shared/Views/MainView.cs) 中，我們不需要編寫複雜的 `<TextBlock>` XML 標籤，而是直接使用 C# 建構子與鏈式擴充方法：

```csharp
// 建立一個垂直排列容器，並設定內含文字與按鈕
new StackPanel()
    .VerticalAlignment(VerticalAlignment.Center)
    .Spacing(24)
    .Children(
        new TextBlock()
            .Text("這是靜態文字"),
        new Button()
            .Content("點擊")
    );
```

### 2. 如何使用 MVVM 進行資料繫結 (Data Binding)？
* **ViewModel 的屬性通知**：在 [ViewModels/MainViewModel.cs](./Avalonia%20Declarative%20UI%20Example.Shared/ViewModels/MainViewModel.cs) 中，只要欄位標記了 `[ObservableProperty]`，原始碼產生器就會在背景產生實作變更通知的公開屬性。
* **View 的強型別繫結**：在 [Views/MainView.cs](./Avalonia%20Declarative%20UI%20Example.Shared/Views/MainView.cs) 中，使用 Lambda 表達式直接綁定 ViewModel 的屬性，享有強型別檢查：
  ```csharp
  new TextBlock()
      .Text(vm, x => x.GreetingText) // 只要 vm.GreetingText 改變，文字框內容就會自動重繪
  ```

### 3. 如何綁定按鈕點擊？
不需使用傳統的 Event Handler，直接使用專屬的擴充方法 `.OnClick(...)` 觸發 ViewModel 內的方法或指令：
```csharp
new Button()
    .Content("點擊更新文字")
    .OnClick(_ => vm.UpdateGreeting()) // 點擊時執行 vm 的 UpdateGreeting 方法
```

### 4. 樣式與 Hover 效果
您可以透過重寫 `BuildStyles()` 來集中管理 UI 的樣式（類似 CSS），以維持畫面的乾淨度：
```csharp
protected override StyleGroup? BuildStyles() =>
[
    new Style<Button>()
        .Background(Brushes.RoyalBlue)
        .CornerRadius(new CornerRadius(8)),
        
    new Style<Button>(x => x.Class(":pointerover")) // 懸停效果
        .Background(Brushes.DodgerBlue)
];
```

### 5. 全域靜態設定 (Config)
在 [Config.cs](./Avalonia%20Declarative%20UI%20Example.Shared/Config.cs) 中集中管理應用程式全域靜態配置，如視窗大小、標題及版本。避免將硬編碼（Hardcoded）的字串散落於各個專案中：

```csharp
// 全域靜態常數
public static string WindowsTitle { get; } = "Avalonia Declarative Template (Desktop)";
public static int WindowWidth { get; } = 800;
```

並在共用啟動檔 [App.cs](./Avalonia%20Declarative%20UI%20Example.Shared/App.cs) 中直接參考：
```csharp
desktop.MainWindow = new Window()
    .Title($"{Config.WindowsTitle} v{Config.Version}")
    .Width(Config.WindowWidth)
    .Height(Config.WindowHeight)
```

### 6. 異步程式設計與狀態繫結 (Async/Await & State Binding)
為了維持 UI 的流暢響應，避免耗時的後台任務（如 API 請求或資料庫操作）凍結使用者介面，我們使用了現代 C# 的 `async/await` 異步程式設計：

* **ViewModel 中的狀態通知與異步任務**：
  在 [MainViewModel.cs](./Avalonia%20Declarative%20UI%20Example.Shared/ViewModels/MainViewModel.cs) 中宣告 `IsBusy` 用以標示當前忙碌狀態，並使用 `async Task` 來定義高負載任務：
  ```csharp
  [ObservableProperty]
  private bool _isBusy;

  [RelayCommand]
  public async Task DoHighLoadTasks()
  {
      IsBusy = true; // 開始處理，進入忙碌狀態
      try
      {
          await GreetingService.HighLoadTasks(); // 異步等待耗時任務
      }
      finally
      {
          IsBusy = false; // 結束忙碌狀態
      }
  }
  ```

* **View 中的狀態繫結 (Binding)**：
  在 [MainView.cs](./Avalonia%20Declarative%20UI%20Example.Shared/Views/MainView.cs) 中，按鈕的啟用狀態 (`IsEnabled`) 直接與 `!IsBusy` 進行繫結。當背景正在執行耗時任務時，按鈕會自動被禁用，防止重複點擊：
  ```csharp
  new Button()
      .Content("執行高負載任務")
      .IsEnabled(vm, x => !x.IsBusy) // 當 IsBusy 為 true 時自動禁用按鈕
      .OnClick(async _ => await vm.DoHighLoadTasks()) // 異步點擊事件觸發
  ```

---

## ⚡ AOT（Ahead-of-Time）編譯的優勢與限制

本模板專案在 Desktop 端設定了 AOT 編譯支援（即 `PublishAot = true`）。

### 🌟 AOT 的主要優勢
1. **極速啟動 (Instant Startup)**：傳統 .NET 應用需要在執行時透過 JIT (Just-In-Time) 編譯器將 MSIL 翻譯成機器碼，而 AOT 在編譯期就已直接編譯為原生機器碼，大幅縮短應用程式啟動時間（這在 Desktop 軟體尤為明顯）。
2. **較低的記憶體佔用 (Low Memory Footprint)**：因為不需要載入 JIT 編譯器及保留大量的編譯元數據 (Metadata)，執行時的實體記憶體佔用顯著降低。
3. **優異的檔案裁剪 (Tree Shaking)**：編譯器在編譯時會進行嚴格的靜態程式碼分析，將沒有被執行路徑碰觸到的程式碼與套件（例如未使用的系統庫）從產出的執行檔中徹底剃除，產出極度輕量且獨立 (Self-contained) 的執行檔。
4. **反編譯防護 (Obfuscation By Default)**：因為輸出為純二進位原生機器碼而非包含豐富元數據的 MSIL，極難被 ILSpy 或 dnSpy 等反編譯工具還原為 C# 原始碼，提高商業智財權的安全性。

### ⚠️ AOT 的限制與注意事項
1. **不支援動態反射 (No Dynamic Reflection)**：AOT 依賴靜態編譯。任何在執行期動態產生程式碼（例如使用 `Reflection.Emit` 或動態解析類型）的程式庫將無法在 AOT 下工作。
2. **依賴注入的裁剪警告 (DI Trimming Warning)**：本專案在 [Program.cs](./Avalonia%20Declarative%20UI%20Example.Desktop/Program.cs) 中使用 `ActivatorUtilities.CreateInstance` 進行建構子注入。由於相依性注入涉及執行期反射，在編譯 AOT 時可能會觸發裁剪警告（例如 `warning IL2067`）。這意味著程式碼樹分析器無法完全保證哪些建構子會被使用。開發時需特別注意不可隨意移除建構子。
3. **跨平台 AOT 的建置管線差異**：雖然桌面端（Windows/macOS/Linux）能非常直觀地產出原生單一檔案執行檔，但在行動端 (Android)，AOT 編譯（如 Xamarin AOT 或 NativeAOT-Android）的打包管線與桌面端不同，需要依賴對應平台的建置工作流工具與設定，因此針對 Android 平台發布時通常以標準 JIT / 預先編譯 (AOT profile-guided) 的方式發布較佳。