using Gantry.Services.FileSystem.Configuration.Abstractions;
using ProtoBuf;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Settings;

[ProtoContract]
[ProtoInclude(100, typeof(MinimalMiniMapClientSettings))]
public abstract class MinimalMiniMapSettings : FeatureSettings
{
    /// <summary>
    ///     Disabled 'V' Key Binding.
    /// </summary>
    [ProtoMember(2)]
    public bool AllowKeyBinding { get; set; }

    /// <summary>
    ///     Disabled 'F6' Key Binding.
    /// </summary>
    [ProtoMember(3)]
    public bool AllowMiniMapKeyBinding { get; set; }

    /// <summary>
    ///     Coordinates HUD disabled by default, and cannot be enabled.
    ///     Disabled "Show Coordinates HUD" in Settings Menu.
    /// </summary>
    [ProtoMember(4)]
    public bool AllowCoordinatesHud { get; set; }

    /// <summary>
    ///     Disabled coordinates being shown when hovering over the mini-map.
    /// </summary>
    [ProtoMember(5)]
    public bool AllowCoordinatesOnHover { get; set; }

    /// <summary>
    ///     Disabled mini-map scrolling.
    /// </summary>
    [ProtoMember(6)]
    public bool AllowMiniMapScrolling { get; set; }

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 6.
    /// </summary>
    [ProtoMember(7)]
    public float MinimumZoomLevel { get; set; }

    /// <summary>
    ///     Mini-map zoom level defaults to 3, rather than 2.
    /// </summary>
    [ProtoMember(8)]
    public float DefaultZoomLevel { get; set; }

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 6.
    /// </summary>
    [ProtoMember(9)]
    public float MaximumZoomLevel { get; set; }

    /// <summary>
    ///     Mini-map visible by default, but can still be manually hidden by player.
    /// </summary>
    [ProtoMember(10)]
    public bool ForceMiniMapActive { get; set; }
}