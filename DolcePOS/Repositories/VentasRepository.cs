using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Threading.Tasks;
using System.Threading;



public class VentasRepository : IVentasRepository
{
        private readonly string _connectionstring;
        public VentasRepository(string connectionstring)
        {

            _connectionstring = connectionstring;
        }
        public async Task<IEnumerable<Ventas>> GetAllAsync(CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        @"select id, cliente_id, metodo_pago_id, fecha, total, impuesto, descuento from ventas", connection
                );
                var result = new List<Ventas>();
                await using var reader = await command.ExecuteReaderAsync(ct);
                while (await reader.ReadAsync(ct))
                   result.Add(Map(reader));


                return result;
        }
        public async Task<Ventas> GetById(int id, CancellationToken ct = default)
        {
              await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        @"select id, cliente_id, metodo_pago_id, fecha, total, impuesto, descuento from ventas where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);

                await using var reader = await command.ExecuteReaderAsync(ct);

                if(await reader.ReadAsync(ct))
                {
                        return Map(reader);
                }


                return null;
        }
        public async Task<bool> CreateAsync(Ventas venta, CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                @"insert into ventas (cliente_id, metodo_pago_id, fecha, total, impuesto, descuento)
                values (@cliente_id, @metodo_pago_id, @fecha, @total, @impuesto, @descuento)", connection
                );
                command.Parameters.AddWithValue("@cliente_id", (object)venta.cliente_id ?? DBNull.Value);
                command.Parameters.AddWithValue("@metodo_pago_id", (object)venta.metodo_pago_id ?? DBNull.Value);
                command.Parameters.AddWithValue("@fecha", (object)venta.fecha ?? DBNull.Value);
                command.Parameters.AddWithValue("@total", venta.total);
                command.Parameters.AddWithValue("@impuesto", venta.impuesto);
                command.Parameters.AddWithValue("@descuento", venta.descuento);

                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;

        }
        public async Task<bool> UpdateAsync(Ventas venta, CancellationToken ct = default)
        {
            await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        @"Update ventas set
                        cliente_id = @cliente_id,
                        metodo_pago_id = @metodo_pago_id,
                        fecha = @fecha,
                        total = @total,
                        impuesto = @impuesto,
                        descuento = @descuento
                        where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", venta.id);
                command.Parameters.AddWithValue("@cliente_id", (object)venta.cliente_id ?? DBNull.Value);
                command.Parameters.AddWithValue("@metodo_pago_id", (object)venta.metodo_pago_id ?? DBNull.Value);
                command.Parameters.AddWithValue("@fecha", (object)venta.fecha ?? DBNull.Value);
                command.Parameters.AddWithValue("@total", venta.total);
                command.Parameters.AddWithValue("@impuesto", venta.impuesto);
                command.Parameters.AddWithValue("@descuento", venta.descuento);

                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
             await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "Delete from ventas where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);

                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
        }
        private static Ventas Map(NpgsqlDataReader reader)
        {
                return new Ventas
                {
                        id = reader.GetInt32(0),
                        cliente_id = reader.GetInt32(1),
                        metodo_pago_id = reader.GetInt32(2),
                        fecha = reader.GetDateTime(3),
                        total = reader.IsDBNull(4) ? 0 : reader.GetFloat(4),
                        impuesto = reader.IsDBNull(5) ? 0 : reader.GetFloat(5),
                        descuento = reader.IsDBNull(6) ? 0 : reader.GetFloat(6)
                };
        }

}