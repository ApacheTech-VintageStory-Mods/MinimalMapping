using System;
using System.Text;
using ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Systems;
using Gantry.Core;
using JetBrains.Annotations;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Patches;

[HarmonyClientSidePatch]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal class MinimalMiniMapClientPatches
{
    /// <summary>
    ///     Disabled 'F6' Key Binding.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(WorldMapManager), "OnLvlFinalize")]
    internal static void Harmony_WorldMapManager_OnLvlFinalize_Postfix()
    {
        if (ApiEx.Side.IsServer()) return;
        try
        {
            if (MinimalMiniMapClientSystem.Settings.AllowMiniMapKeyBinding) return;
            ApiEx.Client.Settings.Bool["showMinimapHud"] = false;
            ApiEx.Client.Input.HotKeys.Remove("worldmaphud");
        }
        catch (Exception ex)
        {
            G.Logger.Error(ex);
        }
    }

    /// <summary>
    ///     Mini-map zoom level defaults to 3, rather than 2.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiElementMap), nameof(GuiElementMap.ComposeElements))]
    internal static void Harmony_GuiElementMap_ComposeElements_Prefix(ref GuiElementMap __instance)
    {
        if (!MinimalMiniMapClientSystem.Settings.Enabled) return;
        __instance.ZoomLevel = MinimalMiniMapClientSystem.Settings.DefaultZoomLevel;
    }

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 6. 
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiElementMap), nameof(GuiElementMap.ZoomAdd))]
    internal static bool Harmony_GuiElementMap_ZoomAdd_Prefix(ref GuiElementMap __instance, float zoomDiff, float px, float pz)
    {
        if (!MinimalMiniMapClientSystem.Settings.Enabled) return true;
        var min = MinimalMiniMapClientSystem.Settings.MinimumZoomLevel;
        var max = MinimalMiniMapClientSystem.Settings.MaximumZoomLevel;
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
    [HarmonyPatch(typeof(GuiElementMap), nameof(GuiElementMap.OnMouseDownOnElement))]
    internal static bool Harmony_GuiElementMap_OnMouseDownOnElement_Prefix(MouseEvent args)
    {
        if (!MinimalMiniMapClientSystem.Settings.Enabled) return true;
        if (MinimalMiniMapClientSystem.Settings.AllowMiniMapScrolling) return true;
        args.Handled = true;
        return false;
    }

    /// <summary>
    ///     Disabled "Show Coordinates HUD" in Settings Menu.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiCompositeSettings), "onCoordinateHudChanged")]
    internal static void Harmony_GuiCompositeSettings_onCoordinateHudChanged_Prefix(ref bool on)
    {
        if (!MinimalMiniMapClientSystem.Settings.Enabled) return;
        if (MinimalMiniMapClientSystem.Settings.AllowCoordinatesHud) return;
        on = false;
    }

    /// <summary>
    ///     Disabled 'F6' Key Binding.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiCompositeSettings), "onMinimapHudChanged")]
    internal static void Harmony_GuiCompositeSettings_onMinimapHudChanged_Prefix(ref bool on)
    {
        if (!MinimalMiniMapClientSystem.Settings.Enabled) return;
        if (MinimalMiniMapClientSystem.Settings.AllowMiniMapKeyBinding) return;
        on = false;
    }    

    /// <summary>
    ///     Disabled coordinates being shown when hovering over the mini-map. 
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiDialogWorldMap), nameof(GuiDialogWorldMap.OnMouseMove))]
    internal static bool Harmony_GuiDialogWorldMap_OnMouseMove_Prefix(ref GuiDialogWorldMap __instance, MouseEvent args)
    {
        if (!MinimalMiniMapClientSystem.Settings.Enabled) return true;
        if (MinimalMiniMapClientSystem.Settings.AllowCoordinatesOnHover) return true;
        if (__instance.SingleComposer is not null && __instance.SingleComposer.Bounds.PointInside(args.X, args.Y))
        {
            var stringBuilder = new StringBuilder();
            var guiElementMap = (GuiElementMap)__instance.SingleComposer.GetElement("mapElem");
            var hoverText = __instance.SingleComposer.GetHoverText("hoverText");
            foreach (var mapLayer in guiElementMap.mapLayers)
            {
                mapLayer.OnMouseMoveClient(args, guiElementMap, stringBuilder);
            }
            hoverText.SetNewText(stringBuilder.ToString().TrimEnd());
        }
        return false;
    }
}