using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public interface IVentasRepository
{
        Task<IEnumerable<Ventas>> GetAllAsync(CancellationToken ct);
        Task<Ventas> GetById(int id, CancellationToken ct);
        Task<bool> CreateAsync(Ventas venta, CancellationToken ct);
        Task<bool> UpdateAsync(Ventas venta, CancellationToken ct);
        Task<bool> DeleteAsync(int id, CancellationToken ct);

}