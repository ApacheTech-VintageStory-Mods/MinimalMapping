using Gantry.Services.EasyX.Dialogue;
using MinimalMapping.Features.MinimalWorldMap.Settings;

namespace MinimalMapping.Features.MinimalWorldMap.Systems;

public class MinimalWorldMapGuiTab(ICoreGantryAPI core, MinimalWorldMapServerSettings settings)
    : EasyXGuiTab<MinimalWorldMapServerSettings>(core, "WorldMap", settings)
{
    private const float MINIMUM_ZOOM_LEVEL = 0.25f;
    private const float MAXIMUM_ZOOM_LEVEL = 6f;

    protected override void ComposeFeatureSettings(GuiComposer composer, ElementBounds left, ElementBounds right)
    {
        // DisableWorldMapKeyBinding
        AddSettingRow(x => x.DisableWorldMapKeyBinding, left, right);

        // DisableMiniMapKeyBinding
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.DisableMiniMapKeyBinding, left, right);

        // WipeWorldMapAtLogin
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.WipeWorldMapAtLogin, left, right);

        // WipeWorldMapOnDeath
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.WipeWorldMapOnDeath, left, right);

        // DisableCoordinatesHud
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.DisableCoordinatesHud, left, right);

        // DisableCoordinatesOnHover
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.DisableCoordinatesOnHover, left, right);

        // DisableMiniMapScrolling
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.DisableMiniMapScrolling, left, right);

        // MinimumZoomLevel
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.MinimumZoomLevel, left, right);

        // MaximumZoomLevel
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.MaximumZoomLevel, left, right);

        // DefaultZoomLevel
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.DefaultZoomLevel, left, right);

        // EnsureMiniMapActive
        SetRowBounds(left, right, out left, out right);
        AddSettingRow(x => x.EnsureMiniMapActive, left, right);
    }

    public override void RefreshValues(GuiComposer composer)
    {
        base.RefreshValues(composer);

        composer.GetSwitch("btnDisableWorldMapKeyBinding").SetValue(Settings.DisableWorldMapKeyBinding);
        composer.GetSwitch("btnDisableMiniMapKeyBinding").SetValue(Settings.DisableMiniMapKeyBinding);

        composer.GetSwitch("btnWipeWorldMapAtLogin").SetValue(Settings.WipeWorldMapAtLogin);
        composer.GetSwitch("btnWipeWorldMapOnDeath").SetValue(Settings.WipeWorldMapOnDeath);

        composer.GetSwitch("btnDisableCoordinatesHud").SetValue(Settings.DisableCoordinatesHud);
        composer.GetSwitch("btnDisableCoordinatesOnHover").SetValue(Settings.DisableCoordinatesOnHover);
        composer.GetSwitch("btnDisableMiniMapScrolling").SetValue(Settings.DisableMiniMapScrolling);

        Settings.MinimumZoomLevel = Math.Clamp(Settings.MinimumZoomLevel, MINIMUM_ZOOM_LEVEL, Settings.MaximumZoomLevel);
        var minSlider = composer.GetSlider<float>("fltMinimumZoomLevel");
        minSlider.SetValues(Settings.MinimumZoomLevel, MINIMUM_ZOOM_LEVEL, MAXIMUM_ZOOM_LEVEL, MINIMUM_ZOOM_LEVEL, 2, "x");

        Settings.MaximumZoomLevel = Math.Clamp(Settings.MaximumZoomLevel, Settings.MinimumZoomLevel, MAXIMUM_ZOOM_LEVEL);
        var maxSlider = composer.GetSlider<float>("fltMaximumZoomLevel");
        maxSlider.SetValues(Settings.MaximumZoomLevel, MINIMUM_ZOOM_LEVEL, MAXIMUM_ZOOM_LEVEL, MINIMUM_ZOOM_LEVEL, 2, "x");

        Settings.DefaultZoomLevel = Math.Clamp(Settings.DefaultZoomLevel, Settings.MinimumZoomLevel, Settings.MaximumZoomLevel);
        var defSlider = composer.GetSlider<float>("fltDefaultZoomLevel");
        defSlider.SetValues(Settings.DefaultZoomLevel, MINIMUM_ZOOM_LEVEL, MAXIMUM_ZOOM_LEVEL, MINIMUM_ZOOM_LEVEL, 2, "x");

        composer.GetSwitch("btnEnsureMiniMapActive").SetValue(Settings.EnsureMiniMapActive);
    }
}