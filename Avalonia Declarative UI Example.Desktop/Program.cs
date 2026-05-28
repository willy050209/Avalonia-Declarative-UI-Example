// filepath: Avalonia Declarative UI Example/Avalonia Declarative UI Example.Desktop/Program.cs
using Avalonia_Declarative_UI_Example.Shared;
using Avalonia_Declarative_UI_Example.Shared.Services;
using Avalonia_Declarative_UI_Example.Shared.Views;
using Avalonia_Declarative_UI_Example.ViewModels;
using Microsoft.Extensions.DependencyInjection;

// ============================================================================
// Desktop (Windows/macOS/Linux) 進入點
// ============================================================================

// 1. 設定相依性注入 (DI 容器)
var services = new ServiceCollection();
services.AddSingleton<GreetingService>();
services.AddTransient<MainViewModel>();
services.AddTransient<MainView>();
var serviceProvider = services.BuildServiceProvider();

// 2. 設定桌面應用程式的生命週期
var lifetime = new ClassicDesktopStyleApplicationLifetime
{
    Args = args,
    ShutdownMode = ShutdownMode.OnLastWindowClose
};

// 3. 配置與啟動 Avalonia 引擎 (指定使用共享的 App 類別)
AppBuilder.Configure<App>()
    .UsePlatformDetect()
    .UseHarfBuzz()
    .UseServiceProvider(serviceProvider)
    .UseComponentControlFactory(type => (Control)ActivatorUtilities.CreateInstance(serviceProvider, type))
    .UseViewInitializationStrategy(ViewInitializationStrategy.Lazy)
    .SetupWithLifetime(lifetime);

// 4. 啟動桌面程式
lifetime.Start(args);
