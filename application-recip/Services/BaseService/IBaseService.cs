using application_recip.Models;
using application_recip.Services.GetBaseService;
using Radzen;

namespace application_recip.Services.BaseService;

public interface IBaseService<T> : IGetBaseService<T>
{
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
