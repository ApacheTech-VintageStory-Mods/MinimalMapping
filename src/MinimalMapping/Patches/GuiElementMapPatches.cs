using Gantry.Services.HarmonyPatches.Annotations;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.MinimalMapping.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
internal class GuiElementMapPatches
{
    /// <summary>
    ///     Mini-map zoom level defaults to 3, rather than 2.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiElementMap), nameof(GuiElementMap.ComposeElements))]
    internal static void Harmony_GuiElementMap_ComposeElements_Prefix(ref GuiElementMap __instance) => __instance.ZoomLevel = 3f;

    /// <summary>
    ///     Mini-map zoom levels now range from 2 to 6, rather than 0.25 to 6. 
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiElementMap), nameof(GuiElementMap.ZoomAdd))]
    internal static bool Harmony_GuiElementMap_ZoomAdd_Prefix(ref GuiElementMap __instance, float zoomDiff, float px, float pz)
    {
        __instance.ZoomLevel = GameMath.Clamp(__instance.ZoomLevel + zoomDiff, 2f, 6f);
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
    ///     Disabled coordinates being shown when hovering over the mini-map. 
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiElementMap), nameof(GuiElementMap.OnMouseDownOnElement))]
    internal static bool Harmony_GuiElementMap_OnMouseDownOnElement_Prefix(MouseEvent args)
    {
        args.Handled = true;
        return false;
    }
}