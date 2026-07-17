using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Runtime.CompilerServices;
using System;



public class IngredientesRepository : IIngredientesRepository
{
        private readonly string _connectionstring;
        public IngredientesRepository(string connectionstring)
        {

            _connectionstring = connectionstring;
        }
        public async Task<IEnumerable<Ingredientes>> GetAllAsync(CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "select id, nombre, cantidad, costo, fecha_vencimiento from ingredientes", connection
                );
                var result = new List<Ingredientes>();
                await using var reader = await command.ExecuteReaderAsync(ct);
                while (await reader.ReadAsync(ct))
                   result.Add(Map(reader));


                return result;
        }
        public async Task<Ingredientes> GetById(int id, CancellationToken ct = default)
        {
              await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "select id, nombre, cantidad, costo, fecha_vencimiento from ingredientes where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);

                await using var reader = await command.ExecuteReaderAsync(ct);

                if(await reader.ReadAsync(ct))
                {
                        return Map(reader);
                }


                return null;
        }
        public async Task<bool> CreateAsync(Ingredientes ingrediente, CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                @"insert into ingredientes (nombre, cantidad, costo, fecha_vencimiento)
                values (@nombre, @cantidad, @costo, @fecha_vencimiento)", connection
                );
                command.Parameters.AddWithValue("@nombre", ingrediente.nombre);
                command.Parameters.AddWithValue("@cantidad", ingrediente.cantidad);
                command.Parameters.AddWithValue("@costo", ingrediente.costo);
                command.Parameters.AddWithValue("@fecha_vencimiento", (object)ingrediente.fecha_vencimiento ?? DBNull.Value);

                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;

        }
        public async Task<bool> UpdateAsync(Ingredientes ingrediente, CancellationToken ct = default)
        {
            await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        @"Update ingredientes set
                        nombre = @nombre,
                        cantidad = @cantidad,
                        costo = @costo,
                        fecha_vencimiento = @fecha_vencimiento
                        where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", ingrediente.id);
                command.Parameters.AddWithValue("@nombre", ingrediente.nombre);
                command.Parameters.AddWithValue("@cantidad", ingrediente.cantidad);
                command.Parameters.AddWithValue("@costo", ingrediente.costo);
                command.Parameters.AddWithValue("@fecha_vencimiento", (object)ingrediente.fecha_vencimiento ?? DBNull.Value);

                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
             await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "Delete from ingredientes where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);

                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
        }
        private static Ingredientes Map(NpgsqlDataReader reader)
        {
                return new Ingredientes
                {
                        id = reader.GetInt32(0),
                        nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                        cantidad = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                        costo = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                        fecha_vencimiento = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)                
                };
        }

}