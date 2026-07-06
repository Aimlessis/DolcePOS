using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Metadata;
using Npgsql;
using System.IO;
using Microsoft.Extensions.Configuration;
using Avalonia.Controls.Primitives;
using System;


namespace DolcePOS;

public partial class MainWindow : Window
{
    private readonly string _connectionstring;
    public MainWindow()
    {
        InitializeComponent();
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

        
        _connectionstring = config.GetConnectionString("DefaultConnection") 
                            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json");
    }

    private void BtnLogin_OnClick(object? sender, RoutedEventArgs e)
    {
        string username = TbxUser.Text;
        string password = TbxPass.Text;
        
        using var connection = new NpgsqlConnection(_connectionstring);
        connection.Open();
        using var command = new NpgsqlCommand("Select password from usuario where nombre = @username", connection);

        command.Parameters.AddWithValue("@username", username);

        var result = command.ExecuteScalar();
        if (result == null)
        {
            Debug.WriteLine("No se encontró el usuario");
            return;
        }
        string storedpassword = result.ToString();

        if (storedpassword == password)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();

        }
        else
        {
            Console.WriteLine("Contraseña incorrecta");
        }
        
       
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
    }
}