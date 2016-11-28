using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class ByondImporter
    {
        struct ByondObjectInfo
        {
            public readonly ByondObjectType Type;
            public readonly List<string> TypePaths;
            public readonly Type RelatedType;

            public ByondObjectInfo(Type type, ByondMapObjectAttribute attribute)
            {
                RelatedType = type;
                TypePaths = attribute.TypePath.ToList();
                Type = attribute.ObjectType;
            }
        }

        struct ByondObjectInfoPair
        {
            public ByondObjectInfo Info;
            public ByondObjectInstanceArgs InstanceArgs;

            public ByondObjectInfoPair(ByondObjectInfo info, ByondObjectInstanceArgs args)
            {
                Info = info;
                InstanceArgs = args;
            }
        }

        class MapImportInfo
        {
            public readonly Dictionary<string, string> DefineHeader;
            public readonly List<List<string>> MapLevels;
            public readonly Dictionary<string, List<ByondObjectInfoPair>> DefinedObjects;

            public MapImportInfo()
            {
                DefineHeader = new Dictionary<string, string>();
                MapLevels = new List<List<string>>();
                DefinedObjects = new Dictionary<string, List<ByondObjectInfoPair>>();
            }
        }

        static readonly MapImportInfo _mapImportInfo = new MapImportInfo();

        static readonly List<ByondObjectInfo> byondObjects = new List<ByondObjectInfo>();

        internal static void Initialize()
        {
            var imported = ModuleManager.ImportedTypes
                .Where(
                type => type.Value.HasAttribute<ByondMapObjectAttribute>()
                );

            foreach (var i in imported)
            {
                byondObjects.Add(
                    new ByondObjectInfo(i.Value,
                    i.Value.TryGetAttribute<ByondMapObjectAttribute>())
                    );
            }          
        }

        internal static Map ImportMap(string path)
        {
            /*
            var buffer = File.ReadAllLines(Path.GetFullPath(path)).ToList();

            var definedTiles = new Dictionary<string, List<ByondObjectInfo>>();
            //var occurances = new Dictionary<ByondObjectInfo, List<string>>();

            int mapStartLine = 0;
            int mapEndLine = 0;
            int bufferLine = 0;

            foreach (string str in buffer)
            {
                bufferLine++;

                if (str.Contains("(1,1,1) = {\""))
                {
                    mapStartLine = bufferLine;
                }
                else if (str.StartsWith("\"}"))
                {
                    mapEndLine = bufferLine;
                }
                else
                {
                    var def = str.Split('(', ',', '{', '}', ')')
                        .Where(@string => @string.StartsWith("/"));

                    var defList = new List<ByondObjectInfo>();

                    byondObjects.ForEach(item =>
                    {
                        foreach (var @string in def)
                        {
                            if (item.TypePaths.Contains(@string))
                                defList.Add(item);
                        }
                    });

                    if (defList.Count > 0)
                        definedTiles.Add(str.Substring(1, 3), defList);
                }
            }

            Map map = new Map();

            var mapBuffer = buffer.GetRange(mapStartLine, mapEndLine - mapStartLine - 1);

            int x, y;

            x = y = 0;

            foreach (string mapLine in mapBuffer)
            {
                x = 0;
                int tiles = mapLine.Length / 3;

                for (int i = 0; i < tiles; i++)
                {
                    var tile = mapLine.Substring(0 + (3 * i), 3);

                    if (definedTiles.ContainsKey(tile))
                        definedTiles[tile].ForEach(type =>
                        {
                            var entity = type.RelatedType.InstantizeAs<Entity>();
                            entity.position.X = x;
                            entity.position.Y = y;
                        });
                    x++;
                }
                y++;
            }
            */

            ParseData(path);
            ReadDefineHeader();
            Map map = BuildMap();
            
            return map;
        }

        internal static void ParseData(string path)
        {
            var buffer = File.ReadAllLines(Path.GetFullPath(path)).ToArray();

            bool mapMode = false;
            List<string> mapLayerBuffer = null;

            foreach (var @string in buffer)
            {
                if (mapMode)
                    mapLayerBuffer.Add(@string);
                if (@string.StartsWith("\"") && !@string.EndsWith("}"))
                {
                    string tile = @string.Split('"')[1];
                    string define = @string.Substring(@string.IndexOf('(')+1);
                    define = define.Remove(define.LastIndexOf(')'));
                    //string define = @string.Split('(', ')')[1];

                    _mapImportInfo.DefineHeader.Add(tile, define);
                }
                else if (@string.StartsWith("("))
                {
                    mapMode = true;
                    mapLayerBuffer = new List<string>(); // start working on the new layer
                }
                else if (@string.StartsWith("\"}"))
                {
                    _mapImportInfo.MapLevels.Add(mapLayerBuffer); // add the layer to the list
                    mapMode = false;
                }  
            }
            
        }

        internal static void ReadDefineHeader()
        {
            foreach (var e in _mapImportInfo.DefineHeader)
            {
                var v = e.Value;
                List<ByondObjectInfoPair> tileInfos = new List<ByondObjectInfoPair>();
                if (e.Key == "abj")
                    Console.WriteLine(v.ToString());
                foreach (var s in v.Split(','))
                {
                    if (e.Key == "abx")
                    {   
                        
                    }
                    var ps = s.Split('{','}'); // split into type path and instance args
                    var p = ps[0];
                    var a = (ps.Length > 1) ? ps[1].Split(';') : null; // args are seperated with ;

                    var l = byondObjects.Where(bobj => p.Contains(bobj.TypePaths[0]));
                    if (!l.Any())
                        continue;

                    var b = l.First();
                    var bt = b.TypePaths[0];

                    Dictionary<string, string> args = new Dictionary<string, string>();

                    if (a != null)
                        foreach (var ae in a) // turn the arg list into a more useful dictionary
                        {
                            if (ae.Length > 0)
                            {
                                var c = ae.Split('=');
                                args.Add(c[0].TrimEnd(), c[1].TrimStart());
                            }
                        }

                    var ext = p.Remove(0,bt.Length);

                    var typePath = new ByondTypePath(bt,p,ext);
                    var tileInfo = new ByondObjectInfoPair(b, new ByondObjectInstanceArgs(typePath, args));

                    tileInfos.Add(tileInfo);
                }
                if (tileInfos.Any())
                    _mapImportInfo.DefinedObjects.Add(e.Key, tileInfos);
            }
            Console.WriteLine(_mapImportInfo.DefinedObjects["abx"]);
        }

        internal static Map BuildMap()
        {
            Map newMap = new Map();

            var definedTiles = _mapImportInfo.DefinedObjects;

            int tileSize = _mapImportInfo.DefinedObjects.First().Key.Length;
            int x, y, z;

            x = y = z = 0;

            foreach (var mapLayerBuffer in _mapImportInfo.MapLevels)
                foreach (string mapLine in mapLayerBuffer)
                {
                    x = 0;
                    int tiles = mapLine.Length / tileSize;

                    for (int i = 0; i < tiles; i++)
                    {
                        var tile = mapLine.Substring(0 + (tileSize * i), tileSize);

                        if (definedTiles.ContainsKey(tile))
                            definedTiles[tile].ForEach(type =>
                            {
                                var entity = type.Info.RelatedType.InstantizeAs<Entity>();
                                entity.PostConstruct(type.InstanceArgs);
                                entity.position.X = x;
                                entity.position.Y = y;
                            });
                        x++;
                    }
                    y++;
                }

            return newMap;
        }
    }
}
