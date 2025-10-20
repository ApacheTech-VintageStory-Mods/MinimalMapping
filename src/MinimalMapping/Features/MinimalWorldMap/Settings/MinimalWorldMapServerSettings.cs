using Newtonsoft.Json.Converters;

namespace MinimalMapping.Features.MinimalWorldMap.Settings;

/// <summary>
///     
/// </summary>
[ProtoContract]
public class MinimalWorldMapServerSettings : FeatureSettings<MinimalWorldMapServerSettings>, IEasyXServerSettings
{
    /// <inheritdoc />
    [NotComposable]
    [ProtoMember(1)]
    [DefaultValue(AccessMode.Enabled)]
    [JsonConverter(typeof(StringEnumConverter))]
    public AccessMode Mode { get; set; } = AccessMode.Enabled;

    /// <inheritdoc />
    [NotComposable]
    [ProtoMember(2)]
    public List<Player> Whitelist { get; set; } = [];

    /// <inheritdoc />
    [NotComposable]
    [ProtoMember(3)]
    public List<Player> Blacklist { get; set; } = [];

    /// <summary>
    ///     Disabled 'M' Key Binding.
    /// </summary>
    [ProtoMember(4)]
    [ChatCommand("worldmap-hotkey")]
    public bool DisableWorldMapKeyBinding { get; set; }

    /// <summary>
    ///     Disabled 'F6' Key Binding.
    /// </summary>
    [ProtoMember(6)]
    [ChatCommand("minimap-hotkey")]
    public bool DisableMiniMapKeyBinding { get; set; }

    /// <summary>
    ///     Wipe the client map whenever the player logs in.
    /// </summary>
    [ProtoMember(7)]
    [ChatCommand("wipe-at-login")]
    public bool WipeWorldMapAtLogin { get; set; }

    /// <summary>
    ///     Wipe the client map whenever the player dies.
    /// </summary>
    [ProtoMember(8)]
    [ChatCommand("wipe-on-death")]
    public bool WipeWorldMapOnDeath { get; set; }

    /// <summary>
    ///     Coordinates HUD disabled by default, and cannot be enabled.
    ///     Disabled "Show Coordinates HUD" in Settings Menu.
    /// </summary>
    [ProtoMember(9)]
    [ChatCommand("coordinates-hud")]
    public bool DisableCoordinatesHud { get; set; }

    /// <summary>
    ///     Disabled coordinates being shown when hovering over the mini-map.
    /// </summary>
    [ProtoMember(10)]
    [ChatCommand("coordinates-hover")]
    public bool DisableCoordinatesOnHover { get; set; }

    /// <summary>
    ///     Disabled mini-map scrolling.
    /// </summary>
    [ProtoMember(11)]
    [ChatCommand("minimap-scroll")]
    public bool DisableMiniMapScrolling { get; set; }

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 12.
    /// </summary>
    [ProtoMember(12)]
    [DefaultValue(0.25f)]
    [ChatCommand("zoom-min", MinValue = 0.25f, MaxValue = 12f, DefaultValue = 0.25f)]
    public float MinimumZoomLevel { get; set; } = 0.25f;

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 12, rather than 0.25 to 12.
    /// </summary>
    [ProtoMember(13)]
    [DefaultValue(12f)]
    [ChatCommand("zoom-max", MinValue = 0.25f, MaxValue = 12f, DefaultValue = 12f)]
    public float MaximumZoomLevel { get; set; } = 12f;

    /// <summary>
    ///     Mini-map zoom level defaults to 3, rather than 2.
    /// </summary>
    [ProtoMember(14)]
    [DefaultValue(3f)]
    [ChatCommand("zoom-default", MinValue = 0.25f, MaxValue = 12f, DefaultValue = 3f)]
    public float DefaultZoomLevel { get; set; } = 3f;

    /// <summary>
    ///     Mini-map visible by default, but can still be manually hidden by player.
    /// </summary>
    [ProtoMember(15)]
    [DefaultValue(true)]
    [ChatCommand("force-minimap")]
    public bool EnsureMiniMapActive { get; set; } = true;
}