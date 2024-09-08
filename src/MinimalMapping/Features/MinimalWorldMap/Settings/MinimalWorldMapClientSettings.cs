using Gantry.Services.EasyX.Abstractions;
using ProtoBuf;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Settings;

[ProtoContract]
public class MinimalWorldMapClientSettings : MinimalWorldMapSettings, IEasyXClientSettings
{
    /// <inheritdoc />
    [ProtoMember(1)]
    public bool Enabled { get; set; }
}