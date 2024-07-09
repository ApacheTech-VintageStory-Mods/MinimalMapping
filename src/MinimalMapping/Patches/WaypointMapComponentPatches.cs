using Gantry.Services.HarmonyPatches.Annotations;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.MinimalMapping.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
internal class WaypointMapComponentPatches
{
    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(WaypointMapComponent), MethodType.Constructor)]
    internal static void Harmony_WaypointMapComponent_Constructor_Prefix(Waypoint waypoint) => waypoint.Pinned = false;
}