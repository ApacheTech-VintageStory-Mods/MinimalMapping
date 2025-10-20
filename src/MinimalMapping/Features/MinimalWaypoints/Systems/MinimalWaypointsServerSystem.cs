using MinimalMapping.Features.MinimalWaypoints.Commands;
using MinimalMapping.Features.MinimalWaypoints.Settings;

namespace MinimalMapping.Features.MinimalWaypoints.Systems;

internal class MinimalWaypointsServerSystem : EasyXServerSystemBase<MinimalWaypointsServerSystem, MinimalWaypointsServerSettings, MinimalWaypointsClientSettings>
{
    protected override string SubCommandName { get; } = "Waypoints";

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);
        api.Event.PlayerJoin += Event_PlayerJoin;
    }

    public override void Dispose()
    {
        base.Dispose();
        Sapi.Event.PlayerJoin -= Event_PlayerJoin;
    }

    private void Event_PlayerJoin(IServerPlayer byPlayer)
    {
        if (!IsEnabledFor(byPlayer) || !Settings.WipeWaypointsAtLogin) return;
        Core.CommandProcessor.Send(new WipeWaypointsCommand { Player = byPlayer });
    }
}