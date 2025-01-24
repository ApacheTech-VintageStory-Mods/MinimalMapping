using System.Collections.Generic;
using Gantry.Core.GameContent.ChatCommands.DataStructures;
using Gantry.Services.EasyX.Abstractions;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Settings;

/// <summary>
///     
/// </summary>
public class MinimalMiniMapServerSettings : MinimalMiniMapSettings, IEasyXServerSettings
{
    /// <inheritdoc />
    public MinimalMiniMapServerSettings()
    {
        MinimumZoomLevel = 2f;
        DefaultZoomLevel = 3f;
        MaximumZoomLevel = 6f;
        EnsureMiniMapActive = false;
        AllowMiniMapKeyBinding = false;
        AllowKeyBinding = true;
    }

    /// <inheritdoc />
    public AccessMode Mode { get; set; } = AccessMode.Enabled;

    /// <inheritdoc />
    public List<Player> Whitelist { get; set; } = [];

    /// <inheritdoc />
    public List<Player> Blacklist { get; set; } = [];
}