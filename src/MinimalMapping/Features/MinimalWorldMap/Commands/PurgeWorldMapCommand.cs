using Gantry.Services.Brighter.Filters;

namespace MinimalMapping.Features.MinimalWorldMap.Commands;

[ClientSide]
public class PurgeWorldMapCommand : CommandBase
{
    [ClientSide]
    public class PurgeWorldMapHandler(ICoreGantryAPI gantry, WorldMapManager worldMapManager) : RequestHandler<PurgeWorldMapCommand>
    {
        private readonly ICoreGantryAPI _gantry = gantry;
        private readonly WorldMapManager _worldMapManager = worldMapManager;

        [HandledOnClient]
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
            var curVisibleChunks = chunkLayer.GetField<HashSet<FastVec2i>>("curVisibleChunks");
            if (curVisibleChunks is null) return false;
            curVisibleChunks.Clear();
            chunkLayer.SetField("curVisibleChunks", curVisibleChunks);

            chunkLayer.ChunkTextures.Clear();
            chunkLayer.LoadedChunks.Clear();

            return true;
        }

        private static bool TryPurgeMapDatabase(PurgeWorldMapCommand command, ChunkMapLayer chunkLayer)
        {
            var mapDb = chunkLayer.GetField<MapDB>("mapdb");
            if (mapDb is null)
            {
                command.Success = false;
                command.ErrorMessages.Add("Could not get map database from chunk layer.");
                return false;
            }

            try
            {
                mapDb.Purge();
                mapDb.Vacuum();
            }
            catch (Exception ex)
            {
                command.Success = false;
                command.ErrorMessages.Add($"Exception purging map database: {ex}");
                return false;
            }
            return true;
        }
    }
}