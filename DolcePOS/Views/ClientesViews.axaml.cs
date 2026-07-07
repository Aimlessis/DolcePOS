using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace DolcePOS;

public partial class ClientesWindow : Window
{
    public ClientesWindow()
    {
        InitializeComponent();
    }

     private void BtnExitToMenu_OnClick(object? sender, RoutedEventArgs e)
    {
        
        MenuWindow menuWindow = new();
        menuWindow.Show();
        Close();


    }
}