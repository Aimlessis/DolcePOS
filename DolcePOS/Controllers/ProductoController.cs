using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ProductoController
{
    
    private IProductoRepository _productoRepository;
    public ProductoController(IProductoRepository productoRepository){
        
        _productoRepository = productoRepository;

    }

    public async Task<IEnumerable<Producto>>GetAllProductoAsync()
    {
        return await _productoRepository.GetAllAsync();
    }

    public async Task<Producto>GetProductoById(int id)
    {
        return await _productoRepository.GetById(id);
    }
    public async Task<bool>CreateProductoAsync(Producto cliente)
    {
        return await _productoRepository.CreateAsync(cliente);
    }
    public async Task<bool>UpdateProductoAsync(Producto cliente)
    {
        return await _productoRepository.UpdateAsync(cliente);
    }
    public async Task<bool>DeleteProductoAsync(int id)
    {
        return await _productoRepository.DeleteAsync(id);
    }
}