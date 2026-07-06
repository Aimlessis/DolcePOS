using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Runtime.CompilerServices;

public class ClientesRepository : IClientesRepository
{
        private readonly string _connectionstring;
        public ClientesRepository(string connectionstring)
        {
            
            _connectionstring = connectionstring;
        }
        public async Task<IEnumerable<Clientes>> GetAllAsync(CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "select id, nombre, telefono, direccion, cantidad, credito from Clientes", connection
                );
                var result = new List<Clientes>();
                await using var reader = await command.ExecuteReaderAsync(ct);
                while (await reader.ReadAsync(ct))
                   result.Add(Map(reader));
                
                
                return result;
        }
        public async Task<Clientes> GetById(int id, CancellationToken ct = default)
        {
              await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "select id, nombre, telefono, direccion, cantidad, credito from Clientes where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);
                
                var reader = await command.ExecuteReaderAsync(ct);

                if(await reader.ReadAsync())
                {
                        return Map(reader);
                }

                
                return null;
        }
        public async Task<bool> CreateAsync(Clientes cliente, CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                @"insert into Clientes (nombre, telefono, direccion, cantidad, credito) 
                values (@nombre, @telefono, @direccion, @cantidad, @credito)", connection
                );
                command.Parameters.AddWithValue("@nombre", cliente.nombre);
                command.Parameters.AddWithValue("@telefono", cliente.telefono);
                command.Parameters.AddWithValue("@direccion", cliente.direccion);
                command.Parameters.AddWithValue("@cantidad", cliente.cantidad);
                command.Parameters.AddWithValue("@credito", cliente.credito);
                
                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
            
        }
        public async Task<bool> UpdateAsync(Clientes cliente, CancellationToken ct = default)    
        {
            await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        @"Update Clientes set 
                        nombre = @nombre, 
                        telefono = @telefono, 
                        direccion = @direccion, 
                        cantidad = @cantidad, 
                        credito = @credito 
                        where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", cliente.id);
                command.Parameters.AddWithValue("@nombre", cliente.nombre);
                command.Parameters.AddWithValue("@telefono", cliente.telefono);
                command.Parameters.AddWithValue("@direccion", cliente.direccion);
                command.Parameters.AddWithValue("@cantidad", cliente.cantidad);
                command.Parameters.AddWithValue("@credito", cliente.credito);
                
                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
             await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "Delete from Clientes where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);
                
                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
        }
        private static Clientes Map(NpgsqlDataReader reader)
        {
                return new Clientes
                {
                        id = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        telefono = reader.IsDBNull(2) ? null : reader.GetString(2),
                        direccion = reader.IsDBNull(3) ? null : reader.GetString(3),
                        cantidad = reader.GetInt32(4),
                        credito = reader.GetFloat(5)
                };
        }

}