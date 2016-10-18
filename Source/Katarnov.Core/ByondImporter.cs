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
            public string Name;
            public ByondObjectType Type;
            public Type RelatedType;
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} has attribute(s)", i);
                Console.WriteLine(i.Value.TryGetAttribute<ByondMapObjectAttribute>().TypePath);
                Console.ResetColor();
            }          
        }

        internal static Map ImportMap(string path)
        {
            /*var buffer = File.ReadAllLines(Path.GetFullPath(path)).ToList();

            List<string> floors = new List<string>();
            List<string> walls = new List<string>();

            int mapStartLine = 0;
            int mapEndLine = 0;
            int bufferLine = 0;

            foreach (string str in buffer)
            {
                bufferLine++;
                if (str.Contains(floor))
                {
                    floors.Add(str.Substring(1, 3));
                    //Console.WriteLine(str.Substring(1, 3));
                }
                else if (str.Contains(wall))
                {
                    walls.Add(str.Substring(1, 3));
                    //Console.WriteLine(str.Substring(1, 3));
                }

                else if (str.Contains("(1,1,1) = {\""))
                {
                    mapStartLine = bufferLine;
                }
                else if (str.Contains("\"}"))
                {
                    mapEndLine = bufferLine;
                }
            }

            Console.WriteLine("Found {0} definiations of Wall in {1}", walls.Count,
                Path.GetFileName(path));
            Console.WriteLine("Found {0} definiations of Floor in {1}", floors.Count,
                Path.GetFileName(path));

            Console.WriteLine("The map starts at {0} and ends at {1}", mapStartLine, mapEndLine);

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

                    if (floors.Contains(tile))
                    {
                        var ent = Global.gameInstance.entityDatabase.
                            GetEntityTypeByName("Floor").InstantizeAs<Entity>();
                        ent.position.X = x;
                        ent.position.Y = y;
                    }
                    else if (walls.Contains(tile))
                    {
                        var ent = Global.gameInstance.entityDatabase.
                            GetEntityTypeByName("Wall").InstantizeAs<Entity>();
                        ent.position.X = x;
                        ent.position.Y = y;
                    }
                    x++;
                }
                y++;
            }
            */
            return new Map();
        }
    }
}
