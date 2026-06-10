using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DolcePOS;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnLogin_OnClick(object? sender, RoutedEventArgs e)
    {
        MenuWindow menu = new MenuWindow();
        menu.Show();
        this.Close();

    }
}