using Gantry.Services.FileSystem.Configuration.Abstractions;
using ProtoBuf;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Settings;

/// <summary>
///     
/// </summary>
[ProtoContract]
[ProtoInclude(100, typeof(MinimalWorldMapClientSettings))]
public abstract class MinimalWorldMapSettings : FeatureSettings
{
    /// <summary>
    ///     Disabled 'M' Key Binding.
    /// </summary>
    [ProtoMember(2)]
    public bool AllowKeyBinding { get; set; }

    /// <summary>
    ///     Wipe the client map whenever the player logs in.
    /// </summary>
    [ProtoMember(3)]
    public bool WipeWorldMapAtLogin { get; set; }

    /// <summary>
    ///     Wipe the client map whenever the player dies.
    /// </summary>
    [ProtoMember(4)]
    public bool WipeWorldMapOnDeath { get; set; }

    /// <summary>
    ///     The radius to display around the player on the world map.
    /// </summary>
    [ProtoMember(5)]
    public int WorldMapFogOfWarRadius { get; set; } = 32;

    /// <summary>
    ///     Only display chunks within a set radius around the player.
    /// </summary>
    [ProtoMember(6)]
    public bool WorldMapFogOfWarEnabled { get; set; }
}