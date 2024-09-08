﻿using Gantry.Services.EasyX.Abstractions;
using Gantry.Services.EasyX.ChatCommands.DataStructures;
using System.Collections.Generic;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Settings;

/// <summary>
///     
/// </summary>
public class MinimalWaypointsServerSettings : MinimalWaypointsSettings, IEasyXServerSettings 
{
    /// <inheritdoc />
    public AccessMode Mode { get; set; } = AccessMode.Enabled;

    /// <inheritdoc />
    public List<Player> Whitelist { get; set; } = [];

    /// <inheritdoc />
    public List<Player> Blacklist { get; set; } = [];
}