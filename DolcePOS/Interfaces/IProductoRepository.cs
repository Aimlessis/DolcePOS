using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface IProductoRepository
{
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Clientes> GetById(int id);
        Task<bool> CreateAsync(Producto producto);
        Task<bool> UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(int id);
}