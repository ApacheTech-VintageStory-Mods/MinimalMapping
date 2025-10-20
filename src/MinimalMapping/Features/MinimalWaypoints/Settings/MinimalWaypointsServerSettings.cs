using Newtonsoft.Json.Converters;

namespace MinimalMapping.Features.MinimalWaypoints.Settings;

/// <summary>
///     
/// </summary>
[ProtoContract]
public class MinimalWaypointsServerSettings : FeatureSettings<MinimalWaypointsServerSettings>, IEasyXServerSettings
{
    /// <inheritdoc />
    [ProtoMember(1)]
    [DefaultValue(AccessMode.Enabled)]
    [JsonConverter(typeof(StringEnumConverter))]
    public AccessMode Mode { get; set; } = AccessMode.Enabled;

    /// <inheritdoc />
    [ProtoMember(2)]
    public List<Player> Whitelist { get; set; } = [];

    /// <inheritdoc />
    [ProtoMember(3)]
    public List<Player> Blacklist { get; set; } = [];

    /// <summary>
    ///     Waypoints cannot be pinned.I've re-written both the '/waypoint' command, and the Add/Edit Waypoint GUIs.
    /// </summary>
    [ProtoMember(4)]
    [DefaultValue(true)]
    [ChatCommand("pins")]
    public bool DisablePinnedWaypoints { get; set; } = true;

    /// <summary>
    ///     Wipe the player's waypoints whenever they log in.
    /// </summary>
    [ProtoMember(5)]
    [ChatCommand("wipe-at-login")]
    public bool WipeWaypointsAtLogin { get; set; }

    /// <summary>
    ///     Wipe the player's waypoints whenever they die.
    /// </summary>
    [ProtoMember(6)]
    [ChatCommand("wipe-on-death")]
    public bool WipeWaypointsOnDeath { get; set; }
}