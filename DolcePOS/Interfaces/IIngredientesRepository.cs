using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface IIngredientesRepository
{
        Task<IEnumerable<Ingredientes>> GetAllAsync(CancellationToken ct = default);
        Task<Ingredientes> GetById(int id, CancellationToken ct = default);
        Task<bool> CreateAsync(Ingredientes ingrediente, CancellationToken ct = default);
        Task<bool> UpdateAsync(Ingredientes ingrediente, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}