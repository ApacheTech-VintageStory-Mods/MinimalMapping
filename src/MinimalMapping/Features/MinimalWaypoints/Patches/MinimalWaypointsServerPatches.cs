using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Systems;
using JetBrains.Annotations;
using System.Collections.Generic;
using Vintagestory.API.Server;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Patches;

[HarmonySidedPatch(EnumAppSide.Server)]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal class MinimalWaypointsServerPatches
{
    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(WaypointMapLayer), "AddWp")]
    internal static void Harmony_WaypointMapLayer_AddWp_Prefix(ref bool pinned, IServerPlayer player)
    {
        if (!MinimalWaypointsServerSystem.IsEnabledFor(player)) return;
        pinned = false;
    }
}