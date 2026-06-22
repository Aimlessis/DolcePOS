using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface IVentasRepository
{
        Task<IEnumerable<Ventas>> GetAllAsync();
        Task<Clientes> GetById(int id);
        Task<bool> CreateAsync(Ventas venta);
        Task<bool> UpdateAsync(Ventas venta);
        Task<bool> DeleteAsync(int id);

}