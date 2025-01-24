using ApacheTech.Common.BrighterSlim;
using Gantry.Core;
using Gantry.Core.Annotation;
using Gantry.Services.Brighter.Filters;
using System.Linq;
using Vintagestory.API.Server;
using Vintagestory.API.Util;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Commands;

[ServerSide]
public class WipeWaypointsAtLoginHandler(WorldMapManager worldMapManager) : RequestHandler<WipeWaypointsAtLoginCommand>
{
    private readonly WorldMapManager _worldMapManager = worldMapManager;

    [HandledOnServer]
    public override WipeWaypointsAtLoginCommand Handle(WipeWaypointsAtLoginCommand command)
    {
        var waypointMapLayer = _worldMapManager.MapLayers.OfType<WaypointMapLayer>().FirstOrDefault();
        if (waypointMapLayer is null) return base.Handle(command);

        var playerWaypoints = waypointMapLayer.Waypoints.Where(p => p.OwningPlayerUid == command.Player.PlayerUID);
        var remainingWaypoints = waypointMapLayer.Waypoints.Except(playerWaypoints).ToList();
        waypointMapLayer.Waypoints = remainingWaypoints;

        ApiEx.Server.WorldManager.SaveGame.StoreData("playerMapMarkers_v2", SerializerUtil.Serialize(remainingWaypoints));
        _worldMapManager.SendMapDataToClient(waypointMapLayer, command.Player as IServerPlayer, []);

        return base.Handle(command);
    }
}
