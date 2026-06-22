using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IClientesRepository
{
        Task<IEnumerable<Clientes>> GetAllAsync();
        Task<Clientes> GetById(int id);
        Task<bool> CreateAsync(Clientes cliente);
        Task<bool> UpdateAsync(Clientes cliente);
        Task<bool> DeleteAsync(int id);


}