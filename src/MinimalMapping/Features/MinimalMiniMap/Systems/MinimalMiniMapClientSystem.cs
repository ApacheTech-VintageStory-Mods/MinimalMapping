using ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Settings;
using Gantry.Services.EasyX.Abstractions;
using System;
using Vintagestory.Client.NoObf;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalMiniMap.Systems;

internal class MinimalMiniMapClientSystem : EasyXClientSystemBase<MinimalMiniMapClientSettings>
{
    public override void StartClientSide(ICoreClientAPI capi)
    {
        base.StartClientSide(capi);
        capi.Event.PlayerJoin += Event_PlayerJoin;
    }

    public override void Dispose()
    {
        GC.SuppressFinalize(this);
        Capi.Event.PlayerJoin -= Event_PlayerJoin;
    }

    private void Event_PlayerJoin(IClientPlayer byPlayer)
    {
        if (!Settings.Enabled) return;

        if (Settings.ForceMiniMapActive)
        {
            Capi.World.Config.SetBool("allowMap", true); // Ensure map is enabled within the game world.
            Capi.Settings.Bool["showMinimapHud"] = true; // Mini-map visible by default, but can still be manually hidden by player. 
        }

        if (!Settings.AllowCoordinatesHud)
        {
            ClientSettings.ShowCoordinateHud = false;   // Coordinates HUD disabled by default, and cannot be enabled.
        }

        if (!Settings.AllowKeyBinding)
        {
            Capi.Input.HotKeys.Remove("coordinateshud"); // Disabled 'V' Key Binding. 
        }
    }
}