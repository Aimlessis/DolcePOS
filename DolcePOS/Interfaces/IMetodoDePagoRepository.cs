using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;


public interface IMetodoDePagoRepository
{
        public Task<IEnumerable<MetodoDePago>> GetAllAsync(CancellationToken ct = default);
        public Task<MetodoDePago> GetById(int id, CancellationToken ct = default);

}