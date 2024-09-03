﻿using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.Users.Drivers;

namespace OrchardCore.Users;

public sealed class ChangeEmailAdminMenu : INavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCore.Settings" },
        { "groupId", ChangeEmailSettingsDisplayDriver.GroupId },
    };

    internal readonly IStringLocalizer S;

    public ChangeEmailAdminMenu(IStringLocalizer<ChangeEmailAdminMenu> localizer)
    {
        S = localizer;
    }

    public ValueTask BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!NavigationHelper.IsAdminMenu(name))
        {
            return ValueTask.CompletedTask;
        }

        builder
            .Add(S["Security"], security => security
                .Add(S["Settings"], settings => settings
                    .Add(S["User Change email"], S["User Change email"].PrefixPosition(), email => email
                        .Permission(CommonPermissions.ManageUsers)
                        .Action("Index", "Admin", _routeValues)
                        .LocalNav()
                    )
                )
            );

        return ValueTask.CompletedTask;
    }
}