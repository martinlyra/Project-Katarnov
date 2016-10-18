using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class ByondImporter
    {
        struct ByondObjectInfo
        {
            public ByondObjectType Type;
            public List<string> TypePaths;
            public Type RelatedType;

            public ByondObjectInfo(Type type, ByondMapObjectAttribute attribute)
            {
                RelatedType = type;
                TypePaths = attribute.TypePath.ToList();
                Type = attribute.ObjectType;
            }
        }

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
            var buffer = File.ReadAllLines(Path.GetFullPath(path)).ToList();

            var definedTiles = new Dictionary<string, List<ByondObjectInfo>>();
            //var occurances = new Dictionary<ByondObjectInfo, List<string>>();

            List<string> floors = new List<string>();
            List<string> walls = new List<string>();

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
            
            return map;
        }
    }
}
