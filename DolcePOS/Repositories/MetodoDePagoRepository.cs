using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Threading.Tasks;
using System.Threading;



public class MetodoDePagoRepository : IMetodoDePagoRepository
{
        private readonly string _connectionstring;
        public MetodoDePagoRepository(string connectionstring)
        {

            _connectionstring = connectionstring;
        }
        public async Task<IEnumerable<MetodoDePago>> GetAllAsync(CancellationToken ct = default)
        {
                await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "select id, nombre from metodo_de_pago", connection
                );
                var result = new List<MetodoDePago>();
                await using var reader = await command.ExecuteReaderAsync(ct);
                while (await reader.ReadAsync(ct))
                   result.Add(Map(reader));


                return result;
        }
        public async Task<MetodoDePago> GetById(int id, CancellationToken ct = default)
        {
              await using var connection = new NpgsqlConnection(_connectionstring);
                await connection.OpenAsync(ct);
                await using var command = new NpgsqlCommand(
                        "select id, nombre from metodo_de_pago where id = @id", connection
                );
                command.Parameters.AddWithValue("@id", id);

                await using var reader = await command.ExecuteReaderAsync(ct);

                if(await reader.ReadAsync(ct))
                {
                        return Map(reader);
                }


                return null;
        }

        private static MetodoDePago Map(NpgsqlDataReader reader)
        {
                return new MetodoDePago
                {
                        id = reader.GetInt32(0),
                        nombre = reader.IsDBNull(1) ? null : reader.GetString(1)
                };
        }

}