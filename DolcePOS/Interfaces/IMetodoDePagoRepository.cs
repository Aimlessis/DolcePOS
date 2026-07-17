using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;


public interface IMetodoDePagoRepository
{
        Task<IEnumerable<MetodoDePago>> GetAllAsync(CancellationToken ct = default);
        Task<MetodoDePago> GetById(int id, CancellationToken ct = default);

}