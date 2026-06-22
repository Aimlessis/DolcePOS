using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IIngredientesRepository
{
        Task<IEnumerable<Ingredientes>> GetAllAsync();
        Task<Clientes> GetById(int id);
        Task<bool> CreateAsync(Ingredientes ingrediente);
        Task<bool> UpdateAsync(Ingredientes ingrediente);
        Task<bool> DeleteAsync(int id);
}