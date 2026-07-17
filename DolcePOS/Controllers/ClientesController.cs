using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ClientesController
{
    
    private IClientesRepository _clientesRepository;
    public ClientesController(IClientesRepository clientesRepository){
        
        _clientesRepository = clientesRepository;

    }

    public async Task<IEnumerable<Clientes>>GetAllClientesAsync()
    {
        return await _clientesRepository.GetAllAsync();
    }

    public async Task<Clientes>GetClienteById(int id)
    {
        return await _clientesRepository.GetById(id);
    }
    public async Task<bool>CreateClienteAsync(Clientes cliente)
    {
        return await _clientesRepository.CreateAsync(cliente);
    }
    public async Task<bool>UpdateClienteAsync(Clientes cliente)
    {
        return await _clientesRepository.UpdateAsync(cliente);
    }
    public async Task<bool>DeleteClienteAsync(int id)
    {
        return await _clientesRepository.DeleteAsync(id);
    }
}