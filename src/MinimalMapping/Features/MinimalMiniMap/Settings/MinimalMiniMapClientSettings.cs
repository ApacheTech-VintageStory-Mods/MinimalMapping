using Gantry.Services.EasyX.Abstractions;
using ProtoBuf;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Settings;

/// <summary>
///     
/// </summary>
[ProtoContract]
public class MinimalMiniMapClientSettings : MinimalMiniMapSettings, IEasyXClientSettings 
{
    /// <inheritdoc />
    [ProtoMember(1)]
    public bool Enabled { get; set; }
}