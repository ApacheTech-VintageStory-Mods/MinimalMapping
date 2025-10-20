using MinimalMapping.Features.MinimalWorldMap.Settings;
using MinimalMapping.Features.MinimalWorldMap.Systems;

namespace MinimalMapping.Features.MinimalWorldMap.Patches;

[HarmonyClientPatch]
public class GuiElementMapPatches : EaxyXClientPatchClass<MinimalWorldMapClientSystem, MinimalWorldMapClientSettings, MinimalWorldMapServerSettings>
{
    /// <summary>
    ///     Mini-map zoom level defaults to 3, rather than 2.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(GuiElementMap), nameof(GuiElementMap.ComposeElements))]
    internal static void Harmony_GuiElementMap_ComposeElements_Prefix(ref GuiElementMap __instance)
    {
        if (Settings is null) return;
        if (!Settings.Enabled) return;

        //
        // Feature: Set Default Zoom Level
        //

        __instance.ZoomLevel = Settings.DefaultZoomLevel;
    }

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 6. 
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(GuiElementMap), nameof(GuiElementMap.ZoomAdd))]
    internal static bool Harmony_GuiElementMap_ZoomAdd_Prefix(ref GuiElementMap __instance, float zoomDiff, float px, float pz)
    {
        if (Settings is null) return true;
        if (!Settings.Enabled) return true;
        
        //
        // Feature: Clamp MiniMap Zoom Levels
        //

        var min = Settings.MinimumZoomLevel;
        var max = Settings.MaximumZoomLevel;

        __instance.ZoomLevel = GameMath.Clamp(__instance.ZoomLevel + zoomDiff, min, max);
        var num = 1f / __instance.ZoomLevel;
        var num2 = __instance.Bounds.InnerWidth * (double)num - __instance.CurrentBlockViewBounds.Width;
        var num3 = __instance.Bounds.InnerHeight * (double)num - __instance.CurrentBlockViewBounds.Length;
        __instance.CurrentBlockViewBounds.X2 += num2;
        __instance.CurrentBlockViewBounds.Z2 += num3;
        __instance.CurrentBlockViewBounds.Translate(-num2 * (double)px, 0.0, -num3 * (double)pz);
        __instance.EnsureMapFullyLoaded();
        return false;
    }

    /// <summary>
    ///     Disabled mini-map scrolling.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(GuiElementMap), nameof(GuiElementMap.OnMouseDownOnElement))]
    internal static bool Harmony_GuiElementMap_OnMouseDownOnElement_Prefix(GuiDialogWorldMap ___worldmapdlg, MouseEvent args)
    {
        if (Settings is null) return true;
        if (!Settings.Enabled) return true;
        if (___worldmapdlg.DialogType == EnumDialogType.Dialog) return true;
        if (args.Button != EnumMouseButton.Left) return true;

        //
        // Feature: Disable MiniMap Scrolling
        //

        if (Settings.DisableMiniMapScrolling)
        {
            args.Handled = true;
            return false;
        }

        return true;
    }
}
