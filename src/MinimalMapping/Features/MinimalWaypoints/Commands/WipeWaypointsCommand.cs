namespace MinimalMapping.Features.MinimalWaypoints.Commands;

public class WipeWaypointsCommand : CommandBase, IPlayerSpecificCommand
{
    public required IPlayer Player { get; set; }
}