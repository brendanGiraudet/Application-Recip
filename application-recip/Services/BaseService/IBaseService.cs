using application_recip.Models;
using Radzen;

namespace application_recip.Services.BaseService;

public interface IBaseService<T>
{
    /// <summary>
    /// Récupere la liste des items filtrés et paginé
    /// </summary>
    /// <returns></returns>
    Task<ODataServiceResult<T>> GetDatagridItemsAsync(LoadDataArgs args, string expand = default(string), string select = default(string), bool? count = default(bool?));

    /// <summary>
    /// Récupere un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<T>> GetItemAsync(Guid id);

    /// <summary>
    /// Créer un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<T>> CreateAsync(T itemToCreate);

    /// <summary>
    /// Met à jour d'un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<T>> UpdateAsync(decimal id, T itemToUpdate);

    /// <summary>
    /// Suppression d'un item
    /// </summary>
    /// <returns></returns>
    Task<MethodResult<decimal>> DeleteAsync(decimal id);
}
