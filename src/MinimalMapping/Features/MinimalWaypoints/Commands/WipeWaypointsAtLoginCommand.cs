using Gantry.Services.Brighter.Abstractions;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Commands;

public class WipeWaypointsAtLoginCommand : CommandBase, IPlayerSpecificCommand
{
    public IPlayer Player { get; set; }
}