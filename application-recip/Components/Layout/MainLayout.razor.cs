using Microsoft.AspNetCore.Components;

namespace application_recip.Components.Layout;

public partial class MainLayout
{
    [Inject] public required IConfiguration Configuration { get; set; }

    private string? ApplicationVersion => Configuration["ApplicationVersion"];

    public bool SidebarExpanded { get; set; } = false;
}
