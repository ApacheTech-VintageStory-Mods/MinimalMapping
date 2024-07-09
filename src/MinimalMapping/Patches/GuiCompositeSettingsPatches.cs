using Gantry.Services.HarmonyPatches.Annotations;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.Client.NoObf;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.MinimalMapping.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
internal class GuiCompositeSettingsPatches
{
    /// <summary>
    ///     Disabled "Show Coordinates HUD" in Settings Menu.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GuiCompositeSettings), "onCoordinateHudChanged")]
    internal static void Harmony_GuiCompositeSettings_onCoordinateHudChanged_Prefix(ref bool on) => on = false;
}