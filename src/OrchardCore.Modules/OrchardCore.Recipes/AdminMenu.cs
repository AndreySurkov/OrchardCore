using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.Security;

namespace OrchardCore.Recipes;

public sealed class AdminMenu : AdminNavigationProvider
{
    internal readonly IStringLocalizer S;

    public AdminMenu(IStringLocalizer<AdminMenu> stringLocalizer)
    {
        S = stringLocalizer;
    }

    protected override ValueTask BuildAsync(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Recipes"], S["Recipes"].PrefixPosition(), recipes => recipes
                    .AddClass("recipes")
                    .Id("recipes")
                    .Permission(StandardPermissions.SiteOwner)
                    .Action("Index", "Admin", "OrchardCore.Recipes")
                    .LocalNav()
                )
            );

        return ValueTask.CompletedTask;
    }
}
