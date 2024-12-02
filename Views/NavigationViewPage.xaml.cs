using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WorkdayCalculator.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NavigationViewPage : Page
{
    private string _namespace => "WorkdayCalculator.Views.";
    public ObservableCollection<CategoryBase> Categories
    {
        get; set;
    }
    public ObservableCollection<CategoryBase> FooterNavItems
    {
        get; set;
    }
    public Microsoft.UI.Xaml.Controls.NavigationView NavigationView => NavigationViewControl;

    public NavigationViewPage()
    {
        this.InitializeComponent();
        //NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First();

        Categories = new ObservableCollection<CategoryBase>();
        Category firstCategory = new Category { Title = "Home", Glyph = Symbol.Home, Tooltip = "Go Home", internalName = "HomePage" };
        Categories.Add(firstCategory);


        Categories.Add(new Category { Title = "My Worktimes", Glyph = Symbol.Calendar, Tooltip = "Add Worktimes", internalName = "WorktimesPage" });
        Categories.Add(new Category { Title = "My Vacationdays", Glyph = Symbol.MapPin, Tooltip = "Add your Vacationdays", internalName = "VacationPage" });
        NavigationViewControl.SelectedItem = firstCategory;

        FooterNavItems = new ObservableCollection<CategoryBase>();
        FooterNavItems.Add(new Category { Title = "Settings", Glyph = Symbol.Keyboard, Tooltip = "This is Settings category", internalName = "SettingsPage" });
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
            contentFrame.Navigate(typeof(SettingsPage));
        }
        else
        {
            Debug.WriteLine("NavView.SelectionChanged Before");

            var selectedItem = (Category)args.SelectedItem;
            // string selectedItemTag = selectedItem.internalName;
            sender.Header = selectedItem.Title;
            string pageName = $"{_namespace}{selectedItem.internalName}";
            Type pageType = Type.GetType(pageName) ?? throw new InvalidOperationException($"Type '{pageName}' not found.");
            contentFrame.Navigate(pageType);
        }
    }

    public void Navigate(
        Type pageType,
        object? targetPageArguments = null,
        Microsoft.UI.Xaml.Media.Animation.NavigationTransitionInfo? navigationTransitionInfo = null)
    {
        NavigationRootPageArgs args = new NavigationRootPageArgs
        {
            NavigationViewPage = this,
            Parameter = targetPageArguments
        };

        contentFrame.Navigate(pageType, targetPageArguments, navigationTransitionInfo);
    }

    public class NavigationRootPageArgs
    {
        public required NavigationViewPage NavigationViewPage;
        public object? Parameter;
    }
    // // [ContentProperty(Name = "ItemTemplate")]
    //private class MenuItemTemplateSelector : DataTemplateSelector
    // {
    //     public DataTemplate ItemTemplate
    //     {
    //         get; set;
    //     }
    //     protected override DataTemplate SelectTemplateCore(object item)
    //     {
    //         return item is Separator ? SeparatorTemplate : item is Header ? HeaderTemplate : ItemTemplate;
    //     }

}
    // }
    public class CategoryBase
    {
    }

    public class Category : CategoryBase
    {
        public string Title {get; set;} = string.Empty;
    
        public required string internalName {get ; set;}
    
        public string Tooltip { get; set;} = string.Empty;
        
        public Symbol Glyph {get; set;}
    }

    public class Separator : CategoryBase
    {
    }

    public class Header : CategoryBase
    {
        public string Name { get; set; } = string.Empty;
    }

// var selectedItem = (Category)args.SelectedItem;
// string selectedItemTag = selectedItem.Name;
// sender.Header = "Sample Page " + selectedItemTag.Substring(selectedItemTag.Length - 1);
// string pageName = "WinUIGallery.SamplePages." + "SamplePage1";
// Type pageType = Type.GetType(pageName);
// contentFrame4.Navigate(pageType);

