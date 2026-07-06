using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Metadata;
using Npgsql;
using Avalonia.Controls.Primitives;
using System;


namespace DolcePOS;

public partial class MainWindow : Window
{
    private readonly string _connectionstring;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnLogin_OnClick(object? sender, RoutedEventArgs e)
    {
        string username = TbxUser.Text;
        string password = TbxPass.Text;
        
        using var connection = new NpgsqlConnection(_connectionstring);
        connection.Open();
        using var command = new NpgsqlCommand("Select password from usuario where username = @username", connection);

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