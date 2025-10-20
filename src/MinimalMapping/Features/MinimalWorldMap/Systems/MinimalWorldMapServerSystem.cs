using MinimalMapping.Features.MinimalWorldMap.Settings;

namespace MinimalMapping.Features.MinimalWorldMap.Systems;

public sealed class MinimalWorldMapServerSystem : EasyXServerSystemBase<MinimalWorldMapServerSystem, MinimalWorldMapServerSettings, MinimalWorldMapClientSettings>
{
    protected override string SubCommandName { get; } = "WorldMap";
}