using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

public interface IProductoRepository
{
        Task<IEnumerable<Producto>> GetAllAsync(CancellationToken ct);
        Task<Producto> GetById(int id, CancellationToken ct);
        Task<bool> CreateAsync(Producto producto, CancellationToken ct);
        Task<bool> UpdateAsync(Producto producto, CancellationToken ct);
        Task<bool> DeleteAsync(int id, CancellationToken ct);
}