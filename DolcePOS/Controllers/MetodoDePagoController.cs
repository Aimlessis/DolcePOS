using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class MetodoDePagoController
{
    
    private IMetodoDePagoRepository _metodoDePagoRepository;
    public MetodoDePagoController(IMetodoDePagoRepository metodoDePagoRepository){
        
        _metodoDePagoRepository = metodoDePagoRepository;

    }

    public async Task<IEnumerable<MetodoDePago>>GetAllMetodoDePagoAsync()
    {
        return await _metodoDePagoRepository.GetAllAsync();
    }

    public async Task<MetodoDePago>GetMetodoDePagoById(int id)
    {
        return await _metodoDePagoRepository.GetById(id);
    }

}