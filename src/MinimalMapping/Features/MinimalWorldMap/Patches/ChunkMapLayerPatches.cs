using MinimalMapping.Features.MinimalWorldMap.Commands;
using MinimalMapping.Features.MinimalWorldMap.Systems;

namespace MinimalMapping.Features.MinimalWorldMap.Patches;

[HarmonyClientPatch]
internal class ChunkMapLayerPatches
{
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(ChunkMapLayer), nameof(ChunkMapLayer.OnLoaded))]
    internal static void Harmony_ChunkMapLayer_OnLoaded_Prefix(ICoreAPI ___api)
    {
        if (___api is not ICoreClientAPI) return;
        var system = ___api.ModLoader.GetModSystem<MinimalWorldMapClientSystem>();
        var settings = system?.Settings;
        if (settings is null) return;
        if (!settings.Enabled) return;

        //
        // Feature: Wipe World Map At Login
        //

        if (settings.WipeWorldMapAtLogin)
        {
            var command = new PurgeWorldMapCommand();
            G.CommandProcessor.Send(command);
            foreach (var errorMessage in command.ErrorMessages)
            {
                G.Logger.Error(errorMessage);
            }
        }
    }
}