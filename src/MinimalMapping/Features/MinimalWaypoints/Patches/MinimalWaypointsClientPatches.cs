using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Systems;
using JetBrains.Annotations;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWaypoints.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal class MinimalWaypointsClientPatches
{
    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(WaypointMapComponent), MethodType.Constructor, typeof(int), typeof(Waypoint), typeof(WaypointMapLayer), typeof(ICoreClientAPI))]
    internal static void Harmony_WaypointMapComponent_Constructor_Prefix(Waypoint waypoint)
    {
        if (MinimalWaypointsClientSystem.Settings.AllowPinnedWaypoints) return;
        waypoint.Pinned = false;
    }

    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiDialogEditWayPoint), "onPinnedToggled")]
    internal static void Harmony_GuiDialogEditWayPoint_onPinnedToggled_Prefix(GuiDialogEditWayPoint __instance, ref bool t1)
    {
        if (MinimalWaypointsClientSystem.Settings.AllowPinnedWaypoints) return;
        t1 = false;
        __instance.SingleComposer.GetSwitch("pinnedSwitch").SetValue(false);
    }

    /// <summary>
    ///     Waypoints cannot be pinned.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiDialogAddWayPoint), "onPinnedToggled")]
    internal static void Harmony_GuiDialogAddWayPoint_onPinnedToggled_Prefix(GuiDialogAddWayPoint __instance, ref bool on)
    {
        if (MinimalWaypointsClientSystem.Settings.AllowPinnedWaypoints) return;
        on = false;
        __instance.SingleComposer.GetSwitch("pinnedSwitch").SetValue(false);
    }
}