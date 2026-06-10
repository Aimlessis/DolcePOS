using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace DolcePOS;

public partial class MenuWindow : Window
{
    public MenuWindow()
    {
        InitializeComponent();
    }

    private void BtnExit_OnClick(object? sender, RoutedEventArgs e)
    {
        
        if(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) desktop.Shutdown();
    }

   }