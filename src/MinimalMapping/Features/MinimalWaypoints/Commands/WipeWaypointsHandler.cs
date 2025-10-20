using Gantry.Services.Brighter.Filters;
using Vintagestory.API.Util;

namespace MinimalMapping.Features.MinimalWaypoints.Commands;

[ServerSide]
public class WipeWaypointsHandler(ICoreServerAPI sapi, WorldMapManager worldMapManager) : RequestHandler<WipeWaypointsCommand>
{
    private readonly ICoreServerAPI _sapi = sapi;
    private readonly WorldMapManager _worldMapManager = worldMapManager;

    [HandledOnServer]
    public override WipeWaypointsCommand Handle(WipeWaypointsCommand command)
    {
        var waypointMapLayer = _worldMapManager.MapLayers.OfType<WaypointMapLayer>().FirstOrDefault();
        if (waypointMapLayer is null) return base.Handle(command);

        var playerWaypoints = waypointMapLayer.Waypoints.Where(p => p.OwningPlayerUid == command.Player.PlayerUID);
        var remainingWaypoints = waypointMapLayer.Waypoints.Except(playerWaypoints).ToList();
        waypointMapLayer.Waypoints = remainingWaypoints;

        _sapi.WorldManager.SaveGame.StoreData("playerMapMarkers_v2", SerializerUtil.Serialize(remainingWaypoints));
        _worldMapManager.SendMapDataToClient(waypointMapLayer, command.Player as IServerPlayer, []);

        return base.Handle(command);
    }
}
