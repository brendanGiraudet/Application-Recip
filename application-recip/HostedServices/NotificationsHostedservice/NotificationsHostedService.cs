using application_recip.Store.MessageStore.Actions;
using Fluxor;

namespace application_recip.HostedServices.NotificationsHostedService;

public class NotificationsHostedService : IHostedService, IDisposable
{
    private readonly Timer _timer;
    private readonly int _pollingInterval;
    private readonly IDispatcher _dispatcher;

    public NotificationsHostedService(
        IConfiguration configuration,
        IDispatcher dispatcher)
    {
        _pollingInterval = int.Parse(configuration["Notifications:PollingInterval"] ?? "5000");

        _timer = new Timer(DoStuff, null, Timeout.Infinite, _pollingInterval);

        _dispatcher = dispatcher;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Change(0, _pollingInterval);
    }

    private void DoStuff(object state)
    {
        _dispatcher.Dispatch(new GetNotificationsAction());
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
