using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface IClientesRepository
{
        public Task<IEnumerable<Clientes>> GetAllAsync(CancellationToken ct = default);
        public Task<Clientes> GetById(int id, CancellationToken ct = default);
        public Task<bool> CreateAsync(Clientes cliente, CancellationToken ct = default);
        public Task<bool> UpdateAsync(Clientes cliente, CancellationToken ct = default);
        public Task<bool> DeleteAsync(int id, CancellationToken ct = default);


}