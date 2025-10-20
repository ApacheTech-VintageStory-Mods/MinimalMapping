namespace MinimalMapping.Features.MinimalWaypoints.Settings;

/// <summary>
///     
/// </summary>
[ProtoContract]
public class MinimalWaypointsClientSettings : IEasyXClientSettings
{
    /// <inheritdoc />
    [ProtoMember(1)]
    public bool Enabled { get; set; }

    /// <summary>
    ///     Waypoints cannot be pinned.I've re-written both the '/waypoint' command, and the Add/Edit Waypoint GUIs.
    /// </summary>
    [ProtoMember(2)]
    [DefaultValue(true)]
    public bool DisablePinnedWaypoints { get; set; } = true;

    /// <summary>
    ///     Wipe the player's waypoints whenever they log in.
    /// </summary>
    [ProtoMember(3)]
    public bool WipeWaypointsAtLogin { get; set; }

    /// <summary>
    ///     Wipe the player's waypoints whenever they die.
    /// </summary>
    [ProtoMember(4)]
    public bool WipeWaypointsOnDeath { get; set; }
}