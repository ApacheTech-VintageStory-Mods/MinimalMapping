using ApacheTech.Common.BrighterSlim;
using ApacheTech.Common.FunctionalCSharp.Extensions;
using Gantry.Core;
using Gantry.Core.Annotation;
using Gantry.Core.Brighter.Abstractions;
using Gantry.Core.Brighter.Filters;
using System.Linq;
using static OpenTK.Graphics.OpenGL.GL;

namespace ApacheTech.VintageMods.MinimalMapping.Features.MinimalWorldMap.Commands;

public class PurgeWorldMapCommand : CommandBase, IPlayerSpecificCommand
{
    public IPlayer Player { get; set; }
}

[ServerSide]
public class PurgeWorldMapHandler : RequestHandler<PurgeWorldMapCommand>
{
    private readonly WorldMapManager _worldMapManager;

    public PurgeWorldMapHandler(WorldMapManager worldMapManager)
    {
        _worldMapManager = worldMapManager;
    }

    [Side(EnumAppSide.Server)]
    public override PurgeWorldMapCommand Handle(PurgeWorldMapCommand command)
    {
        if (!TryPurgeMapDatabase())
        {
            return base.Handle(command);
        }



        return base.Handle(command);
    }

    private bool TryPurgeMapDatabase()
    {
        var chunkLayer = _worldMapManager.MapLayers.OfType<ChunkMapLayer>().FirstOrDefault();
        if (chunkLayer is null) return false;
        var sapi = ApiEx.Server;

        var mapdb = new MapDB(sapi.World.Logger);
        string errorMessage = null;
        string mapdbfilepath = chunkLayer.getMapDbFilePath();
        mapdb.OpenOrCreate(mapdbfilepath, ref errorMessage, true, true, false);
        if (errorMessage is not null)
        {
            sapi.Logger.Error(string.Format("Cannot open {0}, possibly corrupted. Please fix manually or delete this file to continue playing", mapdbfilepath));
            sapi.Logger.Error(errorMessage);
            return false;
        }
        mapdb.Purge();
        return true;
    }
}