using MinimalMapping.Features.MinimalWaypoints.Systems;

namespace MinimalMapping.Features.MinimalWaypoints.Patches;

[HarmonyClientPatch]
internal class MinimalWaypointsClientPatches
{
    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(WaypointMapComponent), MethodType.Constructor, typeof(int), typeof(Waypoint), typeof(WaypointMapLayer), typeof(ICoreClientAPI))]
    internal static void Harmony_WaypointMapComponent_Constructor_Prefix(Waypoint waypoint)
    {
        if (!MinimalWaypointsClientSystem.Instance.Settings.DisablePinnedWaypoints) return;
        waypoint.Pinned = false;
    }

    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(GuiDialogEditWayPoint), "onPinnedToggled")]
    internal static void Harmony_GuiDialogEditWayPoint_onPinnedToggled_Prefix(GuiDialogEditWayPoint __instance, ref bool t1)
    {
        if (!MinimalWaypointsClientSystem.Instance.Settings.DisablePinnedWaypoints) return;
        t1 = false;
        __instance.SingleComposer.GetSwitch("pinnedSwitch").SetValue(false);
    }

    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(GuiDialogAddWayPoint), "onPinnedToggled")]
    internal static void Harmony_GuiDialogAddWayPoint_onPinnedToggled_Prefix(GuiDialogAddWayPoint __instance, ref bool on)
    {
        if (!MinimalWaypointsClientSystem.Instance.Settings.DisablePinnedWaypoints) return;
        on = false;
        __instance.SingleComposer.GetSwitch("pinnedSwitch").SetValue(false);
    }
}