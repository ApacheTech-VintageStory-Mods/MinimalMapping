using ApacheTech.Common.Extensions.Harmony;
using ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Systems;
using Gantry.Core;
using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;
using Vintagestory.API.MathTools;

// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Patches;

[HarmonySidedPatch(EnumAppSide.Client)]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal class MinimalWorldMapClientPatches
{
    /// <summary>
    ///     Disabled 'M' Key Binding.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(WorldMapManager), "OnLvlFinalize")]
    internal static void Harmony_WorldMapManager_OnLvlFinalize_Postfix(ICoreClientAPI ___capi)
    {
        if (MinimalWorldMapClientSystem.Settings.AllowKeyBinding) return;
        ___capi.Input.HotKeys.Remove("worldmapdialog");
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ChunkMapLayer), nameof(ChunkMapLayer.Render))]
    internal static bool Harmony_ChunkMapLayer_Render_Prefix(
        GuiElementMap mapElem,
        float dt,
        ChunkMapLayer __instance,
        ConcurrentDictionary<Vec2i, MultiChunkMapComponent> ___loadedMapData)
    {
        if (!MinimalWorldMapClientSystem.Settings.Enabled) return true;
        if (!MinimalWorldMapClientSystem.Settings.WorldMapFogOfWarEnabled) return true;
        if (!__instance.Active) return true;
        foreach (var multiChunk in ___loadedMapData.Values)
        {
            if (!multiChunk.AnyChunkSet) continue;
            for (int dx = 0; dx < MultiChunkMapComponent.ChunkLen; dx++)
            {
                for (int dy = 0; dy < MultiChunkMapComponent.ChunkLen; dy++)
                {
                    SetOnlyChunksInRange(multiChunk, dx, dy);
                }
            }
            if (!multiChunk.AnyChunkSet) continue;
            multiChunk.Render(mapElem, dt);
        }
        return false;
    }

    private static void SetOnlyChunksInRange(MultiChunkMapComponent multiChunk, int dx, int dy)
    {
        var chunkPos = new Vec2i(multiChunk.chunkCoord.X + dx, multiChunk.chunkCoord.Y + dy);
        var chunkSet = multiChunk.GetField<bool[,]>("chunkSet");
        chunkSet[dx, dy] = multiChunk.IsChunkSet(dx, dy) && !ChunkOutOfRange(chunkPos);
        multiChunk.SetField("chunkSet", chunkSet);
    }

    private static Vec2i PlayerChunk()
    {
        var playerBlockPos = ApiEx.Client.World.Player.Entity.Pos.AsBlockPos;
        return new Vec2i(playerBlockPos.X / 32, playerBlockPos.Z / 32);
    }

    public static bool ChunkOutOfRange(Vec2i chunkPos)
    {
        var renderDistance = MinimalWorldMapClientSystem.Settings.WorldMapFogOfWarRadius;
        var playerChunk = PlayerChunk();
         
        if (playerChunk == chunkPos) return false;
      
        var diff = playerChunk - chunkPos;

        if (Math.Abs(diff.X) > renderDistance) return true;
        if (Math.Abs(diff.Y) > renderDistance) return true;
        return false;
    }

    private static BlockPos CentreChunk(Vec2i chunkPos)
    {
        return new(chunkPos.X * 32 + 16, 0, chunkPos.Y * 32 + 16, 0);
    }
}