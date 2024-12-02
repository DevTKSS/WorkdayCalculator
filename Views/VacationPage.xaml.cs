using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
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
public sealed partial class VacationPage : Page
{
    public ObservableCollection<VacationTime> Vacations { get; set; }
    public VacationPage()
    {
        this.InitializeComponent();
        Vacations = new ObservableCollection<VacationTime>();
    }

    private void AddVacationButton_Click(object sender, RoutedEventArgs e)
    {
        Vacations.Add(new VacationTime
        {
            Id = Vacations.Count + 1,
            Name = "Vacation " + (Vacations.Count + 1),
            StartDate = Convert.ToString(DateTime.Now),
            EndDate = Convert.ToString(DateTime.Now),
            Duration = 0
        });
    }
    private void DeleteVacationButton_Click(object sender, RoutedEventArgs e)
    {
        if (Vacations.Count > 0)
        {
            Vacations.RemoveAt(Vacations.Count - 1);
        }
    }
    private void EditVacationButton_Click(object sender, RoutedEventArgs e)
    {
        if (Vacations.Count > 0)
        {
            var index = Vacations.Count - 1;
            Vacations[index].Name = "Vacation " + (index + 1);
        }
    }
    private void VacationdaysListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        VacationTime vacation = (VacationTime)e.ClickedItem;
        if (vacation != null)
        {
            var index = Vacations.IndexOf(vacation);
            Vacations[index].Duration = Convert.ToInt32((Convert.ToDateTime(Vacations[index].EndDate) - Convert.ToDateTime(Vacations[index].StartDate)).TotalDays);
        }
    }
}
    public class VacationTime
    {
        public int Id
        {
            get; set;
        }
        public string Name { get; set; } = string.Empty;
        public string? StartDate
        {
            get; set;
        }
        public string? EndDate
        {
            get; set;
        }
        public int Duration
        {
            get; set;
        }
    }
