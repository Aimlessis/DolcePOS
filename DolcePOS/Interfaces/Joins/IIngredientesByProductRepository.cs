using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


public interface IIngredientesByProductRepository
{
        Task<IEnumerable<IngredietesXProducto>> GetAllAsync();
        Task<Clientes> GetById(int id);

}