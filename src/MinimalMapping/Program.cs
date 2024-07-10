using ApacheTech.Common.DependencyInjection.Abstractions;
using Gantry.Core.Hosting;
using Gantry.Services.HarmonyPatches.Hosting;
using JetBrains.Annotations;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.Client.NoObf;

namespace ApacheTech.VintageMods.MinimalMapping
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    internal sealed class Program() : ModHost(
#if DEBUG
        debugMode: true
#else
        debugMode: false
#endif
    )
    {
        protected override void ConfigureUniversalModServices(IServiceCollection services, ICoreAPI api)
        {
            services.AddHarmonyPatchingService(api, o => o.AutoPatchModAssembly = true);
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
            api.Event.LevelFinalize += delegate ()
            {
                ClientSettings.ShowCoordinateHud = false;   // Coordinates HUD disabled by default, and cannot be enabled.
                api.World.Config.SetBool("allowMap", true); // Ensure map is enabled within the game world.
                api.Settings.Bool["showMinimapHud"] = true; // Mini-map visible by default, but can still be manually hidden by player. 
                api.Input.HotKeys.Remove("coordinateshud"); // Disabled 'V' Key Binding. 
            };
        }
    }
}
