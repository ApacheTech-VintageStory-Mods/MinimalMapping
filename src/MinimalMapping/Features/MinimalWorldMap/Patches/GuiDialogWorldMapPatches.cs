using MinimalMapping.Features.MinimalWorldMap.Systems;

namespace MinimalMapping.Features.MinimalWorldMap.Patches;

[HarmonyClientPatch]
internal class GuiDialogWorldMapPatches
{
    /// <summary>
    ///     Disabled coordinates being shown when hovering over the mini-map. 
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(GuiDialogWorldMap), nameof(GuiDialogWorldMap.OnMouseMove))]
    internal static bool Harmony_GuiDialogWorldMap_OnMouseMove_Prefix(ref GuiDialogWorldMap __instance, MouseEvent args)
    {
        if (__instance.SingleComposer?.Bounds.PointInside(args.X, args.Y) != true) return true;
        if (!MinimalWorldMapClientSystem.Instance.Settings.Enabled) return true;
        if (!MinimalWorldMapClientSystem.Instance.Settings.DisableCoordinatesOnHover) return true;

        var stringBuilder = new StringBuilder();
        var guiElementMap = __instance.SingleComposer.GetElement("mapElem").To<GuiElementMap>();
        var hoverText = __instance.SingleComposer.GetHoverText("hoverText");
        foreach (var mapLayer in guiElementMap.mapLayers)
        {
            mapLayer.OnMouseMoveClient(args, guiElementMap, stringBuilder);
        }
        hoverText.SetNewText(stringBuilder.ToString().TrimEnd());

        return false;
    }
}