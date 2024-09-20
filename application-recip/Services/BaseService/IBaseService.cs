using application_recip.Models;
using Radzen;

namespace application_recip.Services.BaseService;

public interface IBaseService<T>
{
    /// <summary>
    /// Récupere la liste des items filtrés et paginé
    /// </summary>
    /// <returns></returns>
    Task<ODataServiceResult<T>> GetDatagridItemsAsync(LoadDataArgs args, string? expand = null, string? select = null, bool? count = null);

    /// <summary>
    /// Récupere un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<T>> GetItemAsync(Guid id);

    /// <summary>
    /// Créer un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<T>> CreateAsync(T itemToCreate, string exchangeName, string routingKey);

    /// <summary>
    /// Met à jour d'un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<T>> UpdateAsync(T itemToUpdate, string exchangeName, string routingKey);

    /// <summary>
    /// Suppression d'un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<T>> DeleteAsync(T itemToDelete, string exchangeName, string routingKey);
}
