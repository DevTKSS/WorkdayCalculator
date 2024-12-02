using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace WorkdayCalculator.Helpers;
public class CategoryBase
{
}

public class Category : CategoryBase
{
    public string Name { get; set; } = string.Empty;

    public required string internalName
    {
        get; set;
    }

    public string Tooltip { get; set; } = string.Empty;

    public Symbol Glyph
    {
        get; set;
    }
}

public class Separator : CategoryBase
{
}

public class Header : CategoryBase
{
    public string Name { get; set; } = string.Empty;
}
