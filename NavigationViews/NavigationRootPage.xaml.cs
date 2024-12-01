using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WorkdayCalculator.Views; 

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WorkdayCalculator.NavigationViews;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NavigationRootPage : Page
{
    public NavigationRootPage()
    {
        this.InitializeComponent();
    }

    private void Navigate(NavigationEventArgs e)
    {
        if (e.SourcePageType == typeof(HomePage))
        {
            ContentFrame.Navigate(typeof(HomePage), e.Parameter);
        }
        else if (e.SourcePageType == typeof(SettingsPage))
        {
            ContentFrame.Navigate(typeof(SettingsPage), e.Parameter);
        }
    }
}
