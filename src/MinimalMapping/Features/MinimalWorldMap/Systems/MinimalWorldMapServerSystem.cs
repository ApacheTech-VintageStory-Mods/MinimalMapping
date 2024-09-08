using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Settings;
using Gantry.Core;
using Gantry.Services.EasyX.Abstractions;
using System.Text;
using Vintagestory.API.MathTools;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Systems;

internal class MinimalWorldMapServerSystem : EasyXServerSystemBase<MinimalWorldMapServerSettings, MinimalWorldMapClientSettings, MinimalWorldMapSettings>
{
    protected override string SubCommandName { get; } = "WorldMap";

    protected override void FeatureSpecificCommands(IChatCommand subCommand, CommandArgumentParsers parsers)
    {
        subCommand
            .WithDescription(LangEx.FeatureString(SubCommandName, "Description"));

        // AllowKeyBinding
        subCommand
            .BeginSubCommand("hotkey")
            .WithArgs(parsers.Bool(nameof(Settings.AllowKeyBinding)))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.AllowKeyBinding)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, nameof(Settings.AllowKeyBinding)))
            .EndSubCommand();

        // WipeWorldMapAtLogin
        subCommand
            .BeginSubCommand("wipe")
            .WithArgs(parsers.Bool(nameof(Settings.WipeWorldMapAtLogin)))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.WipeWorldMapAtLogin)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, nameof(Settings.WipeWorldMapAtLogin)))
            .EndSubCommand();

        // WorldMapFogOfWarRadius
        subCommand
            .BeginSubCommand("fogradius")
            .WithArgs(parsers.IntRange(nameof(Settings.WorldMapFogOfWarRadius), 0, 32))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.WorldMapFogOfWarRadius)}", "Description"))
            .HandleWith(args => OnChange<int>(args, nameof(Settings.WorldMapFogOfWarRadius), p => p = GameMath.Clamp(p, 0, 32)))
            .EndSubCommand();

        // WorldMapFogOfWarEnabled
        subCommand
            .BeginSubCommand("fog")
            .WithArgs(parsers.Bool(nameof(Settings.WorldMapFogOfWarEnabled)))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.WorldMapFogOfWarEnabled)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, nameof(Settings.WorldMapFogOfWarEnabled)))
            .EndSubCommand();
    }

    protected override void ExtraDisplayInfo(StringBuilder sb)
    {
        sb.AppendLine(LangEx.FeatureString(SubCommandName, nameof(Settings.AllowKeyBinding), Settings.AllowKeyBinding));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, nameof(Settings.WorldMapFogOfWarRadius), Settings.WorldMapFogOfWarRadius));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, nameof(Settings.WipeWorldMapAtLogin), Settings.WipeWorldMapAtLogin));
    }
}