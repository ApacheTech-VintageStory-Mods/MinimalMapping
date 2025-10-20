using MinimalMapping.Features.MinimalWorldMap.Systems;

namespace MinimalMapping.Features.MinimalWorldMap.Patches;

internal class GuiCompositeSettingsPatches
{
    /// <summary>
    ///     Disabled "Show Coordinates HUD" in Settings Menu.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(GuiCompositeSettings), "onCoordinateHudChanged")]
    internal static void Harmony_GuiCompositeSettings_onCoordinateHudChanged_Prefix(ref bool on)
    {
        if (!MinimalWorldMapClientSystem.Instance.Settings.Enabled) return;
        if (MinimalWorldMapClientSystem.Instance.Settings.DisableCoordinatesHud) return;
        on = false;
    }

    /// <summary>
    ///     Disabled 'F6' Key Binding.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(GuiCompositeSettings), "onMinimapHudChanged")]
    internal static void Harmony_GuiCompositeSettings_onMinimapHudChanged_Prefix(ref bool on)
    {
        if (!MinimalWorldMapClientSystem.Instance.Settings.Enabled) return;
        if (MinimalWorldMapClientSystem.Instance.Settings.DisableMiniMapKeyBinding) return;
        on = false;
    }
}
