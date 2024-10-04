using application_recip.Constants;
using application_recip.Store.GetBaseStore.Actions;
using application_recip.Store.ProfilsStore;
using Fluxor;
using Microsoft.AspNetCore.Components;
using ms_recip.Ms_recip.Models;
using Radzen;

namespace application_recip.Components.Pages.Profils;

public partial class Profils
{
    [Inject] public required IState<ProfilsState> ProfilsState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private void SearchProfils(string searchTerm)
    {
        var loadArgs = new LoadDataArgs();

        loadArgs.Filter = $"contains(tolower({nameof(ProfilModel.Name)}), tolower('{searchTerm}'))";

        Dispatcher.Dispatch(new GetItemsAction<ProfilModel>(loadArgs));
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new GetItemsAction<ProfilModel>(new()));
    }

    private void RedirectToProfilFormPage(Guid? id = null) => NavigationManager.NavigateTo(PageUrlsConstants.GetProfilFormPath(id));
}
