namespace MinimalMapping.Features.MinimalWorldMap.Settings;

[ProtoContract]
public class MinimalWorldMapClientSettings : IEasyXClientSettings
{
    /// <inheritdoc />
    [NotComposable]
    [ProtoMember(1)]
    public bool Enabled { get; set; }

    /// <summary>
    ///     Disabled 'M' Key Binding.
    /// </summary>
    [ProtoMember(2)]
    public bool DisableWorldMapKeyBinding { get; set; }

    /// <summary>
    ///     Disabled 'F6' Key Binding.
    /// </summary>
    [ProtoMember(3)]
    public bool DisableMiniMapKeyBinding { get; set; }

    /// <summary>
    ///     Wipe the client map whenever the player logs in.
    /// </summary>
    [ProtoMember(4)]
    public bool WipeWorldMapAtLogin { get; set; }

    /// <summary>
    ///     Wipe the client map whenever the player dies.
    /// </summary>
    [ProtoMember(5)]
    public bool WipeWorldMapOnDeath { get; set; }

    /// <summary>
    ///     Coordinates HUD disabled by default, and cannot be enabled.
    ///     Disabled "Show Coordinates HUD" in Settings Menu.
    /// </summary>
    [ProtoMember(6)]
    public bool DisableCoordinatesHud { get; set; }

    /// <summary>
    ///     Disabled coordinates being shown when hovering over the mini-map.
    /// </summary>
    [ProtoMember(7)]
    public bool DisableCoordinatesOnHover { get; set; }

    /// <summary>
    ///     Disabled mini-map scrolling.
    /// </summary>
    [ProtoMember(8)]
    public bool DisableMiniMapScrolling { get; set; }

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 6.
    /// </summary>
    [ProtoMember(9)]
    [DefaultValue(0.25f)]
    public float MinimumZoomLevel { get; set; } = 0.25f;

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 6.
    /// </summary>
    [ProtoMember(10)]
    [DefaultValue(12f)]
    public float MaximumZoomLevel { get; set; } = 12f;

    /// <summary>
    ///     Mini-map zoom level defaults to 3, rather than 2.
    /// </summary>
    [ProtoMember(11)]
    [DefaultValue(3f)]
    public float DefaultZoomLevel { get; set; } = 3f;

    /// <summary>
    ///     Mini-map visible by default, but can still be manually hidden by player.
    /// </summary>
    [ProtoMember(12)]
    [DefaultValue(true)]
    public bool EnsureMiniMapActive { get; set; } = true;
}