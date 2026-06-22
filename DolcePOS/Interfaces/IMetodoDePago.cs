using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface IMetodoDePago
{
        Task<IEnumerable<MetodoDePago>> GetAllAsync();
        Task<Clientes> GetById(int id);

}