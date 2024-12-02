using Microsoft.UI.Xaml;
using WorkdayCalculator.Views;
using WorkdayCalculator.Helpers;
using Microsoft.UI.Xaml.Controls;
using WorkdayCalculator.Common;
using System;
using Windows.ApplicationModel.Activation;
using Microsoft.UI.Xaml.Navigation;
using Windows.Graphics;

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

    private static Window? MainWindow;

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs e)
    {
        MainWindow = WindowHelper.CreateWindow();
        MainWindow.ExtendsContentIntoTitleBar = true;

        SizeInt32 size = new SizeInt32(500, 500);
        MainWindow.AppWindow.Resize(size);
        EnsureWindow();
    }

    private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
    }

    private async void EnsureWindow(IActivatedEventArgs? args = null)
    {
        Frame rootFrame = GetRootFrame();
        Type targetPageType = typeof(HomePage);
        string targetPageArguments = string.Empty;

        if (args != null)
        {
            if (args.Kind == ActivationKind.Launch)
            {
                targetPageArguments = ((Windows.ApplicationModel.Activation.LaunchActivatedEventArgs)args).Arguments;
            }
        }
        var eventargs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();
        if (eventargs != null && eventargs.Kind is Microsoft.Windows.AppLifecycle.ExtendedActivationKind.Protocol && eventargs.Data is ProtocolActivatedEventArgs)
        {
            ProtocolActivatedEventArgs ProtocolArgs = (ProtocolActivatedEventArgs)eventargs.Data;
            var uri = ProtocolArgs.Uri.LocalPath.Replace("/", "");

            targetPageArguments = uri;
            //string targetId = string.Empty;
            //if (uri == "AllControls")
            //{
            //    {
            //        targetPageType = typeof(AllControlsPage);
            //    }
            //    else if (uri == "NewControls")
            //    {
            //        targetPageType = typeof(HomePage);
            //    }
            //    else if (ControlInfoDataSource.Instance.Groups.Any(g => g.UniqueId == uri))
            //    {
            //        targetPageType = typeof(SectionPage);
            //    }
            //    else if (ControlInfoDataSource.Instance.Groups.Any(g => g.Items.Any(i => i.UniqueId == uri)))
            //    {
            //        targetPageType = typeof(ItemPage);
            //    }
            //}
        }

        NavigationViewPage rootPage = (NavigationViewPage)MainWindow!.Content as NavigationViewPage;
        rootPage.Navigate(targetPageType, targetPageArguments);

        if (targetPageType == typeof(HomePage))
        {
            ((Microsoft.UI.Xaml.Controls.NavigationViewItem)((NavigationViewPage)App.MainWindow.Content).NavigationView.MenuItems[0]).IsSelected = true;
        }

        MainWindow.Activate();
    }

    private Frame GetRootFrame()
    {
        Frame rootFrame;
        NavigationViewPage? rootPage = MainWindow!.Content as NavigationViewPage;
        if (rootPage == null)
        {
            rootPage = new NavigationViewPage();
            rootFrame = (Frame)rootPage.FindName("rootFrame");
            if (rootFrame == null)
            {
                throw new Exception("Root frame not found");
            }
            rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
            rootFrame.NavigationFailed += OnNavigationFailed;

            MainWindow.Content = rootPage;
        }
        else
        {
            rootFrame = (Frame)rootPage.FindName("rootFrame");
        }
        return rootFrame;
    }
    
}
    ///// Code copy-pasted from the WinUI 3 Windowing sample

    ///// <summary>
    ///// Creates a new Window instance for the application.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void createNewWindow_Click(object sender, RoutedEventArgs e)
    //{
    //    // C# code to create a new window
    //    var newWindow = WindowHelper.CreateWindow();
    //    var rootPage = new NavigationRootPage();
    //    rootPage.RequestedTheme = ThemeHelper.RootTheme;
    //    newWindow.Content = rootPage;
    //    newWindow.Activate();

    //    // C# code to navigate in the new window
    //    var targetPageType = typeof(HomePage);
    //    string targetPageArguments = string.Empty;
    //    rootPage.Navigate(targetPageType, targetPageArguments);
    //}


