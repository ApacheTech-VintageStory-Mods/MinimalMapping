using MinimalMapping.Features.MinimalWorldMap.Commands;
using MinimalMapping.Features.MinimalWorldMap.Settings;
using MinimalMapping.Features.ModMenu.Extensions;

namespace MinimalMapping.Features.MinimalWorldMap.Systems;

public class MinimalWorldMapClientSystem : EasyXClientSystemBase<MinimalWorldMapClientSystem, MinimalWorldMapClientSettings, MinimalWorldMapServerSettings>
{
    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);
        api.AddSettingsTab(() => new MinimalWorldMapGuiTab(Core, ServerSettings));

        if (Settings.EnsureMiniMapActive)
        {
            Capi.World.Config.SetBool("allowMap", true); // Ensure map is enabled within the game world.
            Capi.Settings.Bool.Set("showMinimapHud", value: true, shouldTriggerWatchers: true); // Mini-map visible by default, but can still be manually hidden by player. 
        }

        api.Event.PlayerDeath += Event_PlayerDeath;
        api.Event.PlayerJoin += Event_PlayerJoin;
        api.Event.LevelFinalize += Event_LvlFinalise;
    }

    private void Event_PlayerJoin(IClientPlayer byPlayer)
    {
        if (!Settings.Enabled) return;
        if (byPlayer.PlayerUID != Capi.World.Player.PlayerUID) return;
        if (!Settings.WipeWorldMapAtLogin) return;
        WipeWorldMap();
    }

    private void Event_LvlFinalise()
    {
        if (!Settings.Enabled) return;
        if (Settings.DisableCoordinatesHud)
        {
            ClientSettings.ShowCoordinateHud = false;
        }
        var coordinatesHotkey = Capi.Input.GetHotKeyByCode("coordinateshud");
        var coordinatesHandler = coordinatesHotkey.Handler.Clone().To<ActionConsumable<KeyCombination>>();
        Capi.Input.SetHotKeyHandler(coordinatesHotkey.Code, k => // Ctrl + V
        {
            if (Settings.DisableCoordinatesHud)
            {
                ClientSettings.ShowCoordinateHud = false;
                return false;
            }
            return coordinatesHandler(k);
        });

    }

    private void Event_PlayerDeath(IClientPlayer byPlayer)
    {
        if (!Settings.Enabled) return;
        if (byPlayer.PlayerUID != Capi.World.Player.PlayerUID) return;
        if (!Settings.WipeWorldMapOnDeath) return;
        WipeWorldMap();
    }

    private void WipeWorldMap()
    {
        var command = new PurgeWorldMapCommand();
        Core.CommandProcessor.Send(command);
        foreach (var errorMessage in command.ErrorMessages)
        {
            Core.Logger.Error(errorMessage);
        }
    }

    protected override void OnSettingsChanged()
    {
        base.OnSettingsChanged();
        if (!Settings.Enabled) return;

        if (Settings.DisableCoordinatesHud)
        {
            ClientSettings.ShowCoordinateHud = false;
        }

        if (Settings.DisableMiniMapKeyBinding || Settings.DisableWorldMapKeyBinding)
        {
            var dialogue = Capi.ModLoader.GetModSystem<WorldMapManager>().worldMapDlg;
            dialogue?.TryClose();
        }

        if (Settings.EnsureMiniMapActive)
        {
            Capi.World.Config.SetBool("allowMap", true); // Ensure map is enabled within the game world.
            Capi.Settings.Bool.Set("showMinimapHud", value: true, shouldTriggerWatchers: true); // Mini-map visible by default, but can still be manually hidden by player. 
        }
    }

    public override void Dispose()
    {
        Capi.Event.PlayerDeath -= Event_PlayerDeath;
        Capi.Event.PlayerJoin -= Event_PlayerJoin;
        Capi.Event.LevelFinalize -= Event_LvlFinalise;
    }
}