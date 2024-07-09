using Gantry.Services.HarmonyPatches.Annotations;
using HarmonyLib;
using System.Text;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.MinimalMapping.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
internal class GuiDialogWorldMapPatches
{
    /// <summary>
    ///     Disabled coordinates being shown when hovering over the mini-map. 
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiDialogWorldMap), nameof(GuiDialogWorldMap.OnMouseMove))]
    internal static bool Harmony_GuiDialogWorldMap_OnMouseMove_Prefix(ref GuiDialogWorldMap __instance, MouseEvent args)
    {
        if (__instance.SingleComposer is not null && __instance.SingleComposer.Bounds.PointInside(args.X, args.Y))
        {
            var stringBuilder = new StringBuilder();
            var guiElementMap = (GuiElementMap)__instance.SingleComposer.GetElement("mapElem");
            var hoverText = __instance.SingleComposer.GetHoverText("hoverText");
            foreach (MapLayer mapLayer in guiElementMap.mapLayers)
            {
                mapLayer.OnMouseMoveClient(args, guiElementMap, stringBuilder);
            }
            hoverText.SetNewText(stringBuilder.ToString().TrimEnd());
        }
        return false;
    }
}