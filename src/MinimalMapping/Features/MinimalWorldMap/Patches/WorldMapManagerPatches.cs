using MinimalMapping.Features.MinimalWorldMap.Settings;
using MinimalMapping.Features.MinimalWorldMap.Systems;

namespace MinimalMapping.Features.MinimalWorldMap.Patches;

[HarmonyClientPatch]
internal class WorldMapManagerPatches : EaxyXClientPatchClass<MinimalWorldMapClientSystem, MinimalWorldMapClientSettings, MinimalWorldMapServerSettings>
{
    /// <summary>
    ///     Disabled 'F6' Key Binding.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyClientPatch(typeof(WorldMapManager), "OnLvlFinalize")]
    internal static void Harmony_WorldMapManager_OnLvlFinalize_Postfix(WorldMapManager __instance, ICoreAPI ___api)
    {
        if (___api is not ICoreClientAPI capi) return;
        if (!Settings.Enabled) return;

        //
        // Feature: Disable MiniMap Key Binding
        //

        if (Settings.DisableMiniMapKeyBinding)
            capi.Settings.Bool.Set("showMinimapHud", value: false, shouldTriggerWatchers: true);

        var minimapHotKey = capi.Input.GetHotKeyByCode("worldmaphud");
        var minimapHandler = minimapHotKey.Handler.Clone().To<ActionConsumable<KeyCombination>>();
        capi.Input.SetHotKeyHandler(minimapHotKey.Code, k => // F6
        {
            if (Settings.DisableMiniMapKeyBinding) 
            {
                capi.Settings.Bool.Set("showMinimapHud", value: false, shouldTriggerWatchers: true);
                return false;
            }
            return minimapHandler(k);
        });

        //
        // Feature: Disable World Map Key Binding
        //

        var worldMapHotkey = capi.Input.GetHotKeyByCode("worldmapdialog");
        var worldMapHandler = worldMapHotkey.Handler.Clone().To<ActionConsumable<KeyCombination>>();
        capi.Input.SetHotKeyHandler(worldMapHotkey.Code, k => // M
        {
            if (Settings.DisableWorldMapKeyBinding)
            {
                __instance.worldMapDlg?.TryClose();
                return false;
            }
            return worldMapHandler(k);
        });
    }
}