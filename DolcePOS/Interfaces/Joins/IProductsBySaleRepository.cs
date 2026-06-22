using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


public interface IProductsBySaleRepository
{
        Task<IEnumerable<ProductosXVenta>> GetAllAsync();
        Task<Clientes> GetById(int id);


}