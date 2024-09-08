using System.Collections.Generic;
using Gantry.Services.EasyX.Abstractions;
using Gantry.Services.EasyX.ChatCommands.DataStructures;

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
        ForceMiniMapActive = true;
    }

    /// <inheritdoc />
    public AccessMode Mode { get; set; } = AccessMode.Enabled;

    /// <inheritdoc />
    public List<Player> Whitelist { get; set; } = [];

    /// <inheritdoc />
    public List<Player> Blacklist { get; set; } = [];
}