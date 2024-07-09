using Gantry.Services.HarmonyPatches.Annotations;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.MinimalMapping.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
internal class GuiDialogAddWayPointPatches
{
    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiDialogAddWayPoint), "onPinnedToggled")]
    internal static void Harmony_GuiDialogAddWayPoint_onPinnedToggled_Prefix(ref bool on) => on = false;
}