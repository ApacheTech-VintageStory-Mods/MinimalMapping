using ApacheTech.Common.BrighterSlim;
using ApacheTech.Common.Extensions.Harmony;
using Gantry.Core;
using Gantry.Core.Annotation;
using Gantry.Core.Brighter.Abstractions;
using Gantry.Core.Brighter.Filters;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.MathTools;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Commands;

[ClientSide]
public class PurgeWorldMapCommand : CommandBase
{
    [ClientSide]
    public class PurgeWorldMapHandler(WorldMapManager worldMapManager) : RequestHandler<PurgeWorldMapCommand>
    {
        private readonly WorldMapManager _worldMapManager = worldMapManager;

        [Side(EnumAppSide.Client)]
        public override PurgeWorldMapCommand Handle(PurgeWorldMapCommand command)
        {
            var chunkLayer = _worldMapManager.MapLayers.OfType<ChunkMapLayer>().FirstOrDefault();
            if (chunkLayer is null)
            {
                command.Success = false;
                command.ErrorMessages.Add("Could not get terrain layer from world map manager.");
                return base.Handle(command);
            }

            if (!TryPurgeMapDatabase(command, chunkLayer))
            {
                command.Success = false;
                command.ErrorMessages.Add("Could not purge local map database.");
                return base.Handle(command);
            }

            if (!TryPugeWorldMap(chunkLayer))
            {
                command.Success = false;
                command.ErrorMessages.Add("Could not purge world map textures.");
                return base.Handle(command);
            }

            command.Success = true;
            return base.Handle(command);
        }

        private static bool TryPugeWorldMap(ChunkMapLayer chunkLayer)
        {
            var curVisibleChunks = chunkLayer.GetField<HashSet<Vec2i>>("curVisibleChunks");
            if (curVisibleChunks is null) return false;
            curVisibleChunks.Clear();
            chunkLayer.SetField("curVisibleChunks", curVisibleChunks);

            chunkLayer.ChunkTextures.Clear();
            chunkLayer.LoadedChunks.Clear();

            return true;
        }

        private static bool TryPurgeMapDatabase(PurgeWorldMapCommand command, ChunkMapLayer chunkLayer)
        {
            var sapi = ApiEx.Server;
            var mapdb = new MapDB(sapi.World.Logger);
            string errorMessage = string.Empty;
            string mapdbfilepath = chunkLayer.getMapDbFilePath();
            mapdb.OpenOrCreate(mapdbfilepath, ref errorMessage, true, true, false);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                command.ErrorMessages.Add($"Cannot open `{mapdbfilepath}`, possibly corrupted. Please fix manually or delete this file to continue playing");
                command.ErrorMessages.Add(errorMessage);
                return false;
            }
            mapdb.Purge();
            return true;
        }
    }
}