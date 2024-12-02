using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.UI.Xaml.Controls;
using WorkdayCalculator.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WorkdayCalculator.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NavigationViewPage : Page
{
    private string _namespace => "WorkdayCalculator.Views.";
    public ObservableCollection<CategoryBase> Categories {get; set;}
    public ObservableCollection<CategoryBase> FooterNavItems {get; set;}

    public Microsoft.UI.Xaml.Controls.NavigationView NavigationView => NavigationViewControl;

    public NavigationViewPage()
    {
        this.InitializeComponent();
        NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();

        Categories = new ObservableCollection<CategoryBase>();
        FooterNavItems = new ObservableCollection<CategoryBase>();
        
        Category firstCategory = new Category { Name = "Home", Glyph = Symbol.Home, Tooltip = "Go Home", internalName = "HomePage" };
        Categories.Add(firstCategory);


        Categories.Add(new Category { Name = "My Worktimes", Glyph = Symbol.Calendar, Tooltip = "Add Worktimes", internalName = "WorktimesPage" });
        Categories.Add(new Category { Name = "My Vacationdays", Glyph = Symbol.MapPin, Tooltip = "Add your Vacationdays", internalName = "VacationPage" });
        NavigationViewControl.SelectedItem = firstCategory;

        
        FooterNavItems.Add(new Category { Name = "Settings", Glyph = Symbol.Keyboard, Tooltip = "This is Settings category", internalName = "SettingsPage" });
    }

    public NavigationViewPaneDisplayMode ChoosePanePosition(bool toggleOn)
    {
        if (toggleOn)
        {
            return NavigationViewPaneDisplayMode.Left;
        }
        else
        {
            return NavigationViewPaneDisplayMode.Top;
        }
    }

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            rootFrame.Navigate(typeof(SettingsPage));
        }
        else
        {
            Debug.WriteLine("NavView.SelectionChanged Before");

            var selectedItem = (Category)args.SelectedItem;
            // string selectedItemTag = selectedItem.internalName;
            sender.Header = selectedItem.Name;
            string pageName = $"{_namespace}{selectedItem.internalName}";
            Type pageType = Type.GetType(pageName) ?? throw new InvalidOperationException($"Type '{pageName}' not found.");
            rootFrame.Navigate(pageType);
        }
    }

    public void Navigate(
        Type pageType,
        object? targetPageArguments = null,
        Microsoft.UI.Xaml.Media.Animation.NavigationTransitionInfo? navigationTransitionInfo = null)
    {
        NavigationViewPageArgs args = new NavigationViewPageArgs(this,targetPageArguments);

        rootFrame.Navigate(pageType, args, navigationTransitionInfo);
    }
}
 public class NavigationViewPageArgs
 {
    public NavigationViewPage NavigationViewPage;
    public object? Parameter;
    public NavigationViewPageArgs(NavigationViewPage navigationViewPage, object? parameter)
    {
        NavigationViewPage = navigationViewPage;
        Parameter = parameter;
    }
}



// var selectedItem = (Category)args.SelectedItem;
// string selectedItemTag = selectedItem.Name;
// sender.Header = "Sample Page " + selectedItemTag.Substring(selectedItemTag.Length - 1);
// string pageName = "WinUIGallery.SamplePages." + "SamplePage1";
// Type pageType = Type.GetType(pageName);
// contentFrame4.Navigate(pageType);

