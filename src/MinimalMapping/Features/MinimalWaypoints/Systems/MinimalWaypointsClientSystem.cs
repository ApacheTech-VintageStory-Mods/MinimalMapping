using MinimalMapping.Features.MinimalWaypoints.Settings;
using MinimalMapping.Features.ModMenu.Extensions;

namespace MinimalMapping.Features.MinimalWaypoints.Systems;

internal class MinimalWaypointsClientSystem
    : EasyXClientSystemBase<MinimalWaypointsClientSystem, MinimalWaypointsClientSettings, MinimalWaypointsServerSettings>
{
    public override void StartClientSide(ICoreClientAPI api)
    {
        api.AddSettingsTab(() => new MinimalWaypointsGuiTab(Core, ServerSettings));
        base.StartClientSide(api);
    }
}