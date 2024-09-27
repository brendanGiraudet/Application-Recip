using application_recip.Models;
using application_recip.Services.GetBaseService;

namespace application_recip.Services.SaveBaseservice;

public interface ISaveBaseService<T> : IGetBaseService<T>
{
    Task<MethodResult<IEnumerable<T>>> SaveItemsAsync(IEnumerable<T> items, string exchangeName, string routingKey);
}
