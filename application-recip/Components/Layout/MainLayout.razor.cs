﻿using application_recip.Constants;
using Microsoft.AspNetCore.Components;

namespace application_recip.Components.Layout;

public partial class MainLayout
{
    [Inject] public required IConfiguration Configuration { get; set; }

    private string? ApplicationVersion => Configuration["ApplicationVersion"];

    public bool SidebarExpanded { get; set; } = false;

    IEnumerable<MenuItem> menuItems = Enumerable.Empty<MenuItem>();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        menuItems = menuItems.Append(new MenuItem("Home", MaterialIconConstants.Home, PageUrlsConstants.HomePath));

        menuItems = menuItems.Append(new MenuItem("Recips", MaterialIconConstants.Recips, PageUrlsConstants.RecipsPath));

        menuItems = menuItems.Append(new MenuItem("Ingredients", MaterialIconConstants.Ingredients, PageUrlsConstants.IngredientsPath));

        menuItems = menuItems.Append(new MenuItem("Categories", MaterialIconConstants.Categories, PageUrlsConstants.CategoriesPath));

        menuItems = menuItems.Append(new MenuItem("Profils", MaterialIconConstants.Profils, PageUrlsConstants.ProfilsPath));
    }
}

record MenuItem(string Text, string Icon, string Path);
