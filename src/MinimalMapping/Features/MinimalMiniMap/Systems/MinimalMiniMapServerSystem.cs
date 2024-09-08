using ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Settings;
using Gantry.Core;
using Gantry.Services.EasyX.Abstractions;
using Gantry.Services.EasyX.ChatCommands.Parsers.Extensions;
using System.Text;
using Vintagestory.API.MathTools;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Systems;

internal class MinimalMiniMapServerSystem : EasyXServerSystemBase<MinimalMiniMapServerSettings, MinimalMiniMapClientSettings, MinimalMiniMapSettings>
{
    protected override string SubCommandName { get; } = "MiniMap";

    protected override void FeatureSpecificCommands(IChatCommand subCommand, CommandArgumentParsers parsers)
    {
        subCommand
            .WithDescription(LangEx.FeatureString(SubCommandName, "Description"));

        // AllowKeyBinding
        subCommand
            .BeginSubCommand("hotkey")
            .WithArgs(parsers.Bool("AllowKeyBinding"))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.AllowKeyBinding)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, "AllowKeyBinding"))
            .EndSubCommand();

        // AllowCoordinatesHud
        subCommand
            .BeginSubCommand("hud")
            .WithArgs(parsers.Bool("AllowCoordinatesHud"))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.AllowCoordinatesHud)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, "AllowCoordinatesHud"))
            .EndSubCommand();

        // AllowCoordinatesOnHover
        subCommand
            .BeginSubCommand("hover")
            .WithArgs(parsers.Bool("AllowCoordinatesOnHover"))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.AllowCoordinatesOnHover)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, "AllowCoordinatesOnHover"))
            .EndSubCommand();

        // AllowMiniMapScrolling
        subCommand
            .BeginSubCommand("scroll")
            .WithArgs(parsers.Bool("AllowMiniMapScrolling"))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.AllowMiniMapScrolling)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, "AllowMiniMapScrolling"))
            .EndSubCommand();

        // MinimumZoomLevel
        subCommand
            .BeginSubCommand("min")
            .WithArgs(parsers.FloatRange("MinimumZoomLevel", 0.125f, 12f))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.MinimumZoomLevel)}", "Description"))
            .HandleWith(args => OnChange<float>(args, "MinimumZoomLevel", p => p = GameMath.Clamp(p, 0.1f, Settings.MaximumZoomLevel)))
            .EndSubCommand();

        // DefaultZoomLevel
        subCommand
            .BeginSubCommand("default")
            .WithArgs(parsers.FloatRange("DefaultZoomLevel", 0.25f, 6f))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.DefaultZoomLevel)}", "Description"))
            .HandleWith(args => OnChange<float>(args, "DefaultZoomLevel", p => p = GameMath.Clamp(p, Settings.MinimumZoomLevel, Settings.MaximumZoomLevel)))
            .EndSubCommand();

        // MaximumZoomLevel
        subCommand
            .BeginSubCommand("max")
            .WithArgs(parsers.FloatRange("MaximumZoomLevel", 0.125f, 12f))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.MaximumZoomLevel)}", "Description"))
            .HandleWith(args => OnChange<float>(args, "MaximumZoomLevel", p => p = GameMath.Clamp(p, Settings.MinimumZoomLevel, 12f)))
            .EndSubCommand();

        // ForceMiniMapActive
        subCommand
            .BeginSubCommand("force")
            .WithArgs(parsers.Bool("ForceMiniMapActive"))
            .WithDescription(LangEx.FeatureString($"{SubCommandName}.{nameof(Settings.ForceMiniMapActive)}", "Description"))
            .HandleWith(args => OnChange<bool>(args, "ForceMiniMapActive"))
            .EndSubCommand();
    }

    protected override void ExtraDisplayInfo(StringBuilder sb)
    {
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "AllowKeyBinding", Settings.AllowKeyBinding));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "AllowCoordinatesHud", Settings.AllowCoordinatesHud));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "AllowCoordinatesOnHover", Settings.AllowCoordinatesOnHover));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "AllowMiniMapScrolling", Settings.AllowMiniMapScrolling));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "MinimumZoomLevel", Settings.MinimumZoomLevel));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "DefaultZoomLevel", Settings.DefaultZoomLevel));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "MaximumZoomLevel", Settings.MaximumZoomLevel));
        sb.AppendLine(LangEx.FeatureString(SubCommandName, "ForceMiniMapActive", Settings.ForceMiniMapActive));
    }
}