using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Settings;
using Gantry.Services.FileSystem.Configuration.Abstractions;
using ProtoBuf;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Settings;

/// <summary>
///     
/// </summary>
[ProtoContract]
[ProtoInclude(100, typeof(MinimalWaypointsClientSettings))]
public abstract class MinimalWaypointsSettings : FeatureSettings
{
    /// <summary>
    ///     Waypoints cannot be pinned.I've re-written both the '/waypoint' command, and the Add/Edit Waypoint GUIs.
    /// </summary>
    [ProtoMember(2)]
    public bool AllowPinnedWaypoints { get; set; }

    /// <summary>
    ///     Wipe the player's waypoints whenever they log in.
    /// </summary>
    [ProtoMember(3)]
    public bool WipeWaypointsAtLogin { get; set; }
}