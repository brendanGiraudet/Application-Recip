using application_recip.Models;
using Radzen;

namespace application_recip.Services.GetBaseService;

public interface IGetBaseService<T>
{
    /// <summary>
    /// Récupere la liste des items filtrés et paginé
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<ODataServiceResult<T>>> GetItemsAsync(LoadDataArgs args, string? expand = null, string? select = null, bool? count = null);

    /// <summary>
    /// Récupere un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<T>> GetItemAsync(Guid id);
}
