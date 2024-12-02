using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;

namespace WorkdayCalculator.Common;

[ContentProperty(Name = "ItemTemplate")]
class MenuItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate? ItemTemplate
    {
        get; set;
    }


    protected override DataTemplate? SelectTemplateCore(object item)
    {
        return ItemTemplate; //return item is Separator ? SeparatorTemplate : item is Header ? HeaderTemplate : ItemTemplate;
    }
}
