using Gantry.Services.HarmonyPatches.Annotations;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.MinimalMapping.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
internal class GuiDialogEditWayPointPatches
{
    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiDialogEditWayPoint), "onPinnedToggled")]
    internal static void Harmony_GuiDialogEditWayPoint_onPinnedToggled_Prefix(GuiDialogEditWayPoint __instance, ref bool t1)
    {
        t1 = false;
        __instance.SingleComposer.GetSwitch("pinnedSwitch").SetValue(false);
    }
}