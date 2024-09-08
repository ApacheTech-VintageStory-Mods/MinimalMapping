using Gantry.Services.EasyX.Abstractions;
using ProtoBuf;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Settings;

/// <summary>
///     
/// </summary>
[ProtoContract]
public class MinimalWaypointsClientSettings : MinimalWaypointsSettings, IEasyXClientSettings
{
    /// <inheritdoc />
    [ProtoMember(1)]
    public bool Enabled { get; set; }
}