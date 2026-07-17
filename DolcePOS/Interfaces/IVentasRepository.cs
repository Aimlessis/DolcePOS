using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public interface IVentasRepository
{
        Task<IEnumerable<Ventas>> GetAllAsync(CancellationToken ct = default);
        Task<Ventas> GetById(int id, CancellationToken ct = default);
        Task<bool> CreateAsync(Ventas venta, CancellationToken ct = default);
        Task<bool> UpdateAsync(Ventas venta, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);

}