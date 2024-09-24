using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Commands;
using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Settings;
using Gantry.Core.Hosting;
using Gantry.Services.EasyX.Abstractions;
using Vintagestory.API.Common.Entities;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Systems;

internal class MinimalWorldMapClientSystem : EasyXClientSystemBase<MinimalWorldMapClientSettings>
{
    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);
        api.Event.OnEntityDeath += Event_OnEntityDeath;
    }

    private void Event_OnEntityDeath(Entity entity, DamageSource damageSource)
    {
        if (!Settings.Enabled) return;
        if (!Settings.WipeWorldMapOnDeath) return;
        if (entity is not EntityPlayer entityPlayer) return;
        if (entityPlayer.PlayerUID != Capi.World.Player.PlayerUID) return;

        var command = new PurgeWorldMapCommand();
        IOC.Brighter.Send(command);

        foreach (var errorMessage in command.ErrorMessages)
        {
            entity.Api.Logger.Error(errorMessage);
        }
    }
}