using MinimalMapping.Features.MinimalWaypoints.Commands;
using MinimalMapping.Features.MinimalWaypoints.Systems;

namespace MinimalMapping.Features.MinimalWaypoints.Patches;

[HarmonyServerPatch]
internal class MinimalWaypointsServerPatches
{
    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyServerPatch(typeof(WaypointMapLayer), "AddWp")]
    internal static void Harmony_WaypointMapLayer_AddWp_Prefix(ref bool pinned, IServerPlayer player)
    {
        if (!MinimalWaypointsServerSystem.Instance.IsEnabledFor(player)) return;
        pinned = false;
    }

    [HarmonyPostfix]
    [HarmonyServerPatch(typeof(EntityPlayer), nameof(EntityPlayer.Revive))]
    internal static void Harmony_EntityPlayer_Revive_Postfix(EntityPlayer __instance)
    {
        if (__instance.Player is not IServerPlayer player) return;
        if (!MinimalWaypointsServerSystem.Instance.IsEnabledFor(player)) return;
        if (!MinimalWaypointsServerSystem.Instance.Settings.WipeWaypointsOnDeath) return;
        G.CommandProcessor.Send(new WipeWaypointsCommand { Player = player });
    }
}