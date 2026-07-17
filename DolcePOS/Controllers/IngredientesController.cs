using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class IngredientesController
{
    
    private IIngredientesRepository _ingredientesRepository;
    public IngredientesController(IIngredientesRepository ingredientesRepository){
        
        _ingredientesRepository = ingredientesRepository;

    }

    public async Task<IEnumerable<Ingredientes>>GetAllingredientesAsync()
    {
        return await _ingredientesRepository.GetAllAsync();
    }

    public async Task<Ingredientes>GetingredientesById(int id)
    {
        return await _ingredientesRepository.GetById(id);
    }
    public async Task<bool>CreateingredientesAsync(Ingredientes ingrediente)
    {
        return await _ingredientesRepository.CreateAsync(ingrediente);
    }
    public async Task<bool>UpdateingredientesAsync(Ingredientes ingrediente)
    {
        return await _ingredientesRepository.UpdateAsync(ingrediente);
    }
    public async Task<bool>DeleteingredientesAsync(int id)
    {
        return await _ingredientesRepository.DeleteAsync(id);
    }
}