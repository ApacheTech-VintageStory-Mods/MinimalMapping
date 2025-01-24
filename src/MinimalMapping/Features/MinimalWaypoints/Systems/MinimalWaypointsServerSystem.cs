using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Commands;
using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Settings;
using Gantry.Core;
using Gantry.Core.Hosting;
using Gantry.Services.EasyX.Abstractions;
using System.Text;
using Vintagestory.API.Server;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Systems;

internal class MinimalWaypointsServerSystem : EasyXServerSystemBase<MinimalWaypointsServerSettings, MinimalWaypointsClientSettings, MinimalWaypointsSettings>
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
        IOC.Brighter.Send(new WipeWaypointsAtLoginCommand { Player = byPlayer });
    }

    protected override void FeatureSpecificCommands(IChatCommand subCommand, CommandArgumentParsers parsers)
    {
        subCommand
            .WithDescription(LangEx.FeatureString(SubCommandName, "Description"));

        // AllowPinnedWaypoints
        subCommand
            .BeginSubCommand("pins")
            .WithArgs(parsers.Bool(nameof(Settings.AllowPinnedWaypoints)))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.AllowPinnedWaypoints)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, nameof(Settings.AllowPinnedWaypoints)))
            .EndSubCommand();

        // WipeWaypointsAtLogin
        subCommand
            .BeginSubCommand("wipe")
            .WithArgs(parsers.Bool(nameof(Settings.WipeWaypointsAtLogin)))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.WipeWaypointsAtLogin)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, nameof(Settings.WipeWaypointsAtLogin)))
            .EndSubCommand();
    }

    protected override void ExtraDisplayInfo(StringBuilder sb)
    {
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "AllowPinnedWaypoints", Settings.AllowPinnedWaypoints));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "WipeWaypointsAtLogin", Settings.WipeWaypointsAtLogin));
    }
}