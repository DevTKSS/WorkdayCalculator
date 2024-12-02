//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.Storage;

/// Copy from WinUI 3 Galley App Repo with adjusted Project Namespace
/// removed Win32 operations and added a helper method to get the local folder
namespace WorkdayCalculator.Helpers;

// Helper class to allow the app to find the Window that contains an
// arbitrary UIElement (GetWindowForElement).  To do this, we keep track
// of all active Windows.  The app code must call WindowHelper.CreateWindow
// rather than "new Window" so we can keep track of all the relevant
// windows.  In the future, we would like to support this in platform APIs.
public class WindowHelper
{
    public static Window CreateWindow()
    {
        Window newWindow = new Window
        {
            SystemBackdrop = new MicaBackdrop()
        };
        TrackWindow(newWindow);
        return newWindow;
    }

    public static void TrackWindow(Window window)
    {
        window.Closed += (sender,args) =>  _activeWindows.Remove(window);
                   
        _activeWindows.Add(window);
    }

    public static AppWindow GetAppWindow(Window window)
    {
        return window.AppWindow;
    }

    /// <summary>
    /// Get the Window that contains the given UIElement.
    /// Will return null if no window is found.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static Window? GetWindowForElement(UIElement element)
    {
        if (element.XamlRoot != null)
        {
            foreach (Window window in _activeWindows)
            {
                if (element.XamlRoot == window.Content.XamlRoot)
                {
                    return window;
                }
            }
        }
        return null;
    }

    // get dpi for an element
    public static double GetRasterizationScaleForElement(UIElement element)
    {
        if (element.XamlRoot != null)
        {
            foreach (Window window in _activeWindows)
            {
                if (element.XamlRoot == window.Content.XamlRoot)
                {
                    return element.XamlRoot.RasterizationScale;
                }
            }
        }
        return 0.0;
    }

    public static List<Window> ActiveWindows => _activeWindows;

    private static readonly List<Window> _activeWindows = new List<Window>();

    public static bool isPackaged()
    {
        return System.AppContext.BaseDirectory.Contains("WindowsApps");
    }

    public static StorageFolder GetAppLocalFolder()
    {

        StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        if (isPackaged())
        {
            localFolder = Task.Run(async () => await StorageFolder.GetFolderFromPathAsync(System.AppContext.BaseDirectory)).Result;
        }
        else
        {
            localFolder = ApplicationData.Current.LocalFolder;
        }
        return localFolder;
    }
}
