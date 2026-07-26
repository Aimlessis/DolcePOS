using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DolcePOS;

public partial class ClientesWindow : Window
{
    private readonly ClientesController _clientesController;
    private Clientes? _selectedCliente;
    private List<Clientes> _clientesList = new();

    public ClientesWindow()
    {
        InitializeComponent();
        
        // Load configuration and initialize repository/controller
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

        string connectionString = config.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'");
        
        // Initialize repository and controller
        var clientesRepository = new ClientesRepository(connectionString);
        _clientesController = new ClientesController(clientesRepository);
        
        // Load clientes when window opens
        _ = LoadClientesAsync();
    }

    private async void BtnExitToMenu_OnClick(object? sender, RoutedEventArgs e)
    {
        MenuWindow menuWindow = new();
        menuWindow.Show();
        Close();
    }

    // Create - Guardar Cliente button click handler
    private async void BtnGuardarCliente_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!ValidateInputs())
            return;

        try
        {
            bool success;
            
            if (_selectedCliente != null)
            {
                // Update existing cliente
                _selectedCliente.nombre = TbxName.Text ?? string.Empty;
                _selectedCliente.telefono = TbxPhoneNumber.Text ?? string.Empty;
                _selectedCliente.direccion = TbxDirection.Text ?? string.Empty;
                _selectedCliente.credito = float.TryParse(TbxCredit.Text, out float credito) ? credito : 0;
                
                success = await _clientesController.UpdateClienteAsync(_selectedCliente);
            }
            else
            {
                // Create new cliente
                var newCliente = new Clientes
                {
                    nombre = TbxName.Text ?? string.Empty,
                    telefono = TbxPhoneNumber.Text ?? string.Empty,
                    direccion = TbxDirection.Text ?? string.Empty,
                    credito = float.TryParse(TbxCredit.Text, out float credito) ? credito : 0,
                };
                
                success = await _clientesController.CreateClienteAsync(newCliente);
            }

            if (success)
            {
                ClearInputs();
                await LoadClientesAsync();
                // You could add a success message here
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar cliente: {ex.Message}");
            // Handle error (show message to user)
        }
    }

    // Delete - Eliminar Cliente button click handler
    private async void BtnEliminarCliente_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_selectedCliente == null)
        {
            // Show message: Por favor selecciona un cliente para eliminar
            return;
        }

        try
        {
            bool success = await _clientesController.DeleteClienteAsync(_selectedCliente.id);
            
            if (success)
            {
                ClearInputs();
                await LoadClientesAsync();
                // You could add a success message here
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar cliente: {ex.Message}");
            // Handle error
        }
    }

    // Load all clientes from database
    private async Task LoadClientesAsync()
    {
        try
        {
            _clientesList = (await _clientesController.GetAllClientesAsync()).ToList();
            Console.WriteLine($"Loaded {_clientesList.Count} clientes");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar clientes: {ex.Message}");
        }
    }

    // Validate user inputs
    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(TbxName.Text))
        {
            // Show error: El nombre es obligatorio
            return false;
        }

        if (!float.TryParse(TbxCredit.Text, out _) && !string.IsNullOrWhiteSpace(TbxCredit.Text))
        {
            // Show error: El crédito debe ser un número válido
            return false;
        }

        return true;
    }

    // Clear all input fields
    private void ClearInputs()
    {
        TbxName.Text = string.Empty;
        TbxPhoneNumber.Text = string.Empty;
        TbxDirection.Text = string.Empty;
        TbxCredit.Text = string.Empty;
        _selectedCliente = null;
    }

    // Helper method to load selected cliente into inputs for editing
    private void LoadClienteForEdit(Clientes cliente)
    {
        _selectedCliente = cliente;
        TbxName.Text = cliente.nombre;
        TbxPhoneNumber.Text = cliente.telefono;
        TbxDirection.Text = cliente.direccion;
        TbxCredit.Text = cliente.credito.ToString();
    }

    // // DataGrid selection changed handler - loads selected cliente into edit form
    // private void ClientesDataGrid_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    // {
    //     if (ClientesDataGrid.SelectedItem is Clientes selectedCliente)
    //     {
    //         LoadClienteForEdit(selectedCliente);
    //     }
    // }
}