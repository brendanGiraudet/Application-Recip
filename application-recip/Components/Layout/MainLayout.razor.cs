using Microsoft.AspNetCore.Components;

namespace application_recip.Components.Layout;

public partial class MainLayout
{
    [Inject] public IConfiguration Configuration { get; set; }

    private string _applicationVersion => Configuration["ApplicationVersion"];

    public bool SidebarExpanded { get; set; } = false;
}
