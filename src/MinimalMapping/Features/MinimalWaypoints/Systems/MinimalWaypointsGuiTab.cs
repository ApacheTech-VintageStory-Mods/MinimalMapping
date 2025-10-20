using Gantry.Services.EasyX.Dialogue;
using MinimalMapping.Features.MinimalWaypoints.Settings;

namespace MinimalMapping.Features.MinimalWaypoints.Systems;

public class MinimalWaypointsGuiTab(ICoreGantryAPI core, MinimalWaypointsServerSettings settings)
    : EasyXGuiTab<MinimalWaypointsServerSettings>(core, "Waypoints", settings)
{
    protected override void ComposeFeatureSettings(GuiComposer composer, ElementBounds left, ElementBounds right)
    {
        // DisablePinnedWaypoints
        composer
            .AddStaticText(F("lblDisablePinnedWaypoints.Text"), LabelFont, EnumTextOrientation.Right, left, "lblDisablePinnedWaypoints")
            .AddAutoSizeHoverText(F("lblDisablePinnedWaypoints.HoverText"), HoverTextFont, HOVER_TEXT_WIDTH, left)
            .AddSwitch(OnDisablePinnedWaypointsChanged, right, "btnDisablePinnedWaypoints");

        // WipeWaypointsAtLogin
        SetRowBounds(left, right, out left, out right);
        composer
            .AddStaticText(F("lblWipeWaypointsAtLogin.Text"), LabelFont, EnumTextOrientation.Right, left, "lblWipeWaypointsAtLogin")
            .AddAutoSizeHoverText(F("lblWipeWaypointsAtLogin.HoverText"), HoverTextFont, HOVER_TEXT_WIDTH, left)
            .AddSwitch(OnWipeWaypointsAtLoginChanged, right, "btnWipeWaypointsAtLogin");

        // WipeWaypointsOnDeath
        SetRowBounds(left, right, out left, out right);
        composer
            .AddStaticText(F("lblWipeWaypointsOnDeath.Text"), LabelFont, EnumTextOrientation.Right, left, "lblWipeWaypointsOnDeath")
            .AddAutoSizeHoverText(F("lblWipeWaypointsOnDeath.HoverText"), HoverTextFont, HOVER_TEXT_WIDTH, left)
            .AddSwitch(OnWipeWaypointsOnDeathChanged, right, "btnWipeWaypointsOnDeath");
    }

    public override void RefreshValues(GuiComposer composer)
    {
        base.RefreshValues(composer);
        composer.GetSwitch("btnDisablePinnedWaypoints").SetValue(Settings.DisablePinnedWaypoints);
        composer.GetSwitch("btnWipeWaypointsAtLogin").SetValue(Settings.WipeWaypointsAtLogin);
        composer.GetSwitch("btnWipeWaypointsOnDeath").SetValue(Settings.WipeWaypointsOnDeath);
    }

    private void OnDisablePinnedWaypointsChanged(bool state)
    {
        Settings.DisablePinnedWaypoints = state;
        RefreshValues(Composer);
    }

    private void OnWipeWaypointsAtLoginChanged(bool state)
    {
        Settings.WipeWaypointsAtLogin = state;
        RefreshValues(Composer);
    }

    private void OnWipeWaypointsOnDeathChanged(bool state)
    {
        Settings.WipeWaypointsOnDeath = state;
        RefreshValues(Composer);
    }
}