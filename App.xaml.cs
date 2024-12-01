﻿using Microsoft.UI.Xaml;
using WorkdayCalculator.Views;
using WorkdayCalculator.NavigationViews;
using WorkdayCalculator.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WorkdayCalculator;
/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    private static Window? startupWindow;

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {

        startupWindow = new MainWindow();
        startupWindow.ExtendsContentIntoTitleBar = true;
        startupWindow.Activate();

    }

    /// Code copy-pasted from the WinUI 3 Windowing sample
    
    /// <summary>
    /// Creates a new Window instance for the application.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void createNewWindow_Click(object sender, RoutedEventArgs e)
    {
        // C# code to create a new window
        var newWindow = WindowHelper.CreateWindow();
        var rootPage = new NavigationRootPage();
        rootPage.RequestedTheme = ThemeHelper.RootTheme;
        newWindow.Content = rootPage;
        newWindow.Activate();

        // C# code to navigate in the new window
        var targetPageType = typeof(HomePage);
        string targetPageArguments = string.Empty;
        rootPage.Navigate(targetPageType, targetPageArguments);
    }
}
