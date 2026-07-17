using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class VentasController
{
    
    private IVentasRepository _VentasRepository;
    public VentasController(IVentasRepository VentasRepository){
        
        _VentasRepository = VentasRepository;

    }

    public async Task<IEnumerable<Ventas>>GetAllVentasAsync()
    {
        return await _VentasRepository.GetAllAsync();
    }

    public async Task<Ventas>GetVentasById(int id)
    {
        return await _VentasRepository.GetById(id);
    }
    public async Task<bool>CreateVentasAsync(Ventas cliente)
    {
        return await _VentasRepository.CreateAsync(cliente);
    }
    public async Task<bool>UpdateVentasAsync(Ventas cliente)
    {
        return await _VentasRepository.UpdateAsync(cliente);
    }
    public async Task<bool>DeleteVentasAsync(int id)
    {
        return await _VentasRepository.DeleteAsync(id);
    }
}