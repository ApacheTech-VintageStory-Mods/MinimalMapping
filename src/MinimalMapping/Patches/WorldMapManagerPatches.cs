using Gantry.Services.HarmonyPatches.Annotations;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.MinimalMapping.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
internal class WorldMapManagerPatches
{
    /// <summary>
    ///     Disabled 'M' Key Binding.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(WorldMapManager), "OnLvlFinalize")]
    internal static void Harmony_WorldMapManager_OnLvlFinalize_Postfix(ICoreClientAPI ___capi) 
        => ___capi.Input.HotKeys.Remove("worldmapdialog");
}