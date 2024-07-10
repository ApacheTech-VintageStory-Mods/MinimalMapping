using Gantry.Services.HarmonyPatches.Annotations;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;
using static OpenTK.Graphics.OpenGL.GL;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.MinimalMapping.Patches;

[HarmonySidedPatch(EnumAppSide.Server)]
internal class WaypointMapLayerPatches
{
    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(WaypointMapLayer), "AddWp")]
    internal static void Harmony_WaypointMapLayer_AddWp_Prefix(ref bool pinned) => pinned = false;
}