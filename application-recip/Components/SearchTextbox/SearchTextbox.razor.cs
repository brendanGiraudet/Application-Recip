using Microsoft.AspNetCore.Components;

namespace application_recip.Components.SearchTextbox;

public partial class SearchTextbox : IDisposable
{
    [Parameter] public EventCallback<string?> LaunchSearchCallback { get; set; }
    [Parameter] public string Name { get; set; }

    private Timer _timer = default!;
    private string? _searchTerm;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _timer = new Timer(OnUserFinish, null, Timeout.Infinite, Timeout.Infinite);
    }
    
    void ResetTimer(ChangeEventArgs e)
    {
        _searchTerm = e.Value?.ToString();
        _timer.Change(1000, Timeout.Infinite); // Démarre le timer
    }

    private async void OnUserFinish(Object? state)
    {
        // Execute the callback inside blazor context
        await InvokeAsync(async () =>
        {
            if (LaunchSearchCallback.HasDelegate)
            {
                await LaunchSearchCallback.InvokeAsync(_searchTerm);
            }
        });
    }

    public void Dispose() => _timer?.Dispose();
}
