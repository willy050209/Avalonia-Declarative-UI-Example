// filepath: Avalonia Declarative UI Example/Avalonia Declarative UI Example.Android/MainApplication.cs
using System;
using Android.App;
using Android.Runtime;
using Avalonia;
using Avalonia.Android;
using Avalonia.Markup.Declarative;
using Avalonia.Themes.Fluent;
using Avalonia_Declarative_UI_Example.Shared;
using Avalonia_Declarative_UI_Example.Shared.Services;
using Avalonia_Declarative_UI_Example.Shared.Views;
using Avalonia_Declarative_UI_Example.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Avalonia_Declarative_UI_Example.Android;

/// <summary>
/// Android 應用程式進入點。
/// 繼承自 AvaloniaAndroidApplication&lt;App&gt;，負責初始化 Android 環境與相依性注入容器。
/// </summary>
[Application]
public class MainApplication : AvaloniaAndroidApplication<App>
{
    public MainApplication(IntPtr handle, JniHandleOwnership transfer)
        : base(handle, transfer)
    {
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        // 1. 初始化 Android 端的相依性注入
        var services = new ServiceCollection();
        services.AddSingleton<GreetingService>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainView>();
        var serviceProvider = services.BuildServiceProvider();

        // 2. 配置 Avalonia 引擎並繫結服務
        return base.CustomizeAppBuilder(builder)
            .UseHarfBuzz()
            .UseServiceProvider(serviceProvider)
            .UseComponentControlFactory(type => (Control)ActivatorUtilities.CreateInstance(serviceProvider, type))
            .UseViewInitializationStrategy(ViewInitializationStrategy.Lazy);
    }
}
