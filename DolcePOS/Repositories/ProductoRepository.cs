using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Threading.Tasks;
using System.Threading;


public class ProductoRepository : IProductoRepository
{
        private readonly string _connectionstring;
        public ProductoRepository(string connectionstring)
        {

            _connectionstring = connectionstring;
        }
        public async Task<IEnumerable<Producto>> GetAllAsync(CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        @"select id, nombre_producto, costo, precio, cantidad_producto,
                        descuento_max, beneficio, descripcion, categoria from productos", connection
                );
                var result = new List<Producto>();
                await using var reader = await command.ExecuteReaderAsync(ct);
                while (await reader.ReadAsync(ct))
                   result.Add(Map(reader));


                return result;
        }
        public async Task<Producto> GetById(int id, CancellationToken ct = default)
        {
              await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        @"select id, nombre_producto, costo, precio, cantidad_producto,
                        descuento_max, beneficio, descripcion, categoria from productos where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);

                await using var reader = await command.ExecuteReaderAsync(ct);

                if(await reader.ReadAsync(ct))
                {
                        return Map(reader);
                }


                return null;
        }
        public async Task<bool> CreateAsync(Producto producto, CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                @"insert into productos (nombre_producto, costo, precio, cantidad_producto, descuento_max, beneficio, descripcion)
                values (@nombre_producto, @costo, @precio, @cantidad_producto, @descuento_max, @beneficio, @descripcion)", connection
                );
                command.Parameters.AddWithValue("@nombre_producto", producto.nombre);
                command.Parameters.AddWithValue("@costo", producto.costo);
                command.Parameters.AddWithValue("@precio", producto.precio);
                command.Parameters.AddWithValue("@cantidad_producto", producto.cantidad);
                command.Parameters.AddWithValue("@descuento_max", producto.descuento_max);
                command.Parameters.AddWithValue("@beneficio", producto.beneficio);
                command.Parameters.AddWithValue("@descripcion", (object)producto.descripcion ?? DBNull.Value);
               

                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;

        }
        public async Task<bool> UpdateAsync(Producto producto, CancellationToken ct = default)
        {
            await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        @"Update productos set
                        nombre_producto = @nombre_producto,
                        costo = @costo,
                        precio = @precio,
                        cantidad_producto = @cantidad_producto,
                        descuento_max = @descuento_max,
                        beneficio = @beneficio,
                        descripcion = @descripcion,
                        where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", producto.id);
                command.Parameters.AddWithValue("@nombre_producto", producto.nombre);
                command.Parameters.AddWithValue("@costo", producto.costo);
                command.Parameters.AddWithValue("@precio", producto.precio);
                command.Parameters.AddWithValue("@cantidad_producto", producto.cantidad);
                command.Parameters.AddWithValue("@descuento_max", producto.descuento_max);
                command.Parameters.AddWithValue("@beneficio", producto.beneficio);
                command.Parameters.AddWithValue("@descripcion", (object)producto.descripcion ?? DBNull.Value);


                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
             await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "Delete from productos where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);

                int rowsaffected = await command.ExecuteNonQueryAsync(ct);

                return rowsaffected > 0;
        }
        private static Producto Map(NpgsqlDataReader reader)
        {
                return new Producto
                {
                        id = reader.GetInt32(0),
                        nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                        costo = reader.IsDBNull(2) ? 0 : reader.GetFloat(2),
                        precio = reader.IsDBNull(3) ? 0 : reader.GetFloat(3),
                        cantidad = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                        descuento_max = reader.IsDBNull(5) ? 0 : reader.GetFloat(5),
                        beneficio = reader.IsDBNull(6) ? 0 : reader.GetFloat(6),
                        descripcion = reader.IsDBNull(7) ? null : reader.GetString(7),
                        
                };
        }

}