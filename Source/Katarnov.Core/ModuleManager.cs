using Katarnov.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    static class ModuleManager
    {
        static readonly Dictionary<string, KeyValuePair<IModule, Assembly>> moduleList = 
            new Dictionary<string, KeyValuePair<IModule, Assembly>>();

        static readonly Dictionary<string, Type> importedTypes =
            new Dictionary<string, Type>(); 

        static readonly string moduleFolder = "Modules";

        internal static void Initialize()
        {
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);

            List<string> files = Directory.GetFiles(moduleFolder).ToList();

            foreach (string file in files)
            {
                try
                {
                    if (Path.GetFileNameWithoutExtension(file) == "Katarnov.Module" 
                        || Path.GetExtension(file) != ".dll")
                        continue;

                    Assembly assembly = Assembly.LoadFrom(file);

                    Console.WriteLine(assembly.GetName().Name);

                    foreach (var t in assembly.GetExportedTypes())
                    {
                        if (t.BaseType == typeof(EntityDefine))
                        {
                            var ed = (EntityDefine)Activator.CreateInstance(t);

                            Global.gameInstance.entityDatabase.internalDatabase.Add(t.Name, ed);
                            Console.WriteLine("-> Imported {0}", t.Name, 
                                assembly.GetName().Name);
                        }
                        if (t.Inherits<Entity>())
                        {
                            Console.Write("{0} :", t.Name);
                            t.GetTypeInfo().GetInheritedBaseTypes().ToList().ForEach(
                                o => Console.Write(" -> {0}", o));
                            Console.Write("\n");
                            importedTypes.Add(t.Name, t);
                            Global.gameInstance.entityDatabase.AddEntityType(t);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        internal static void LoadModules()
        {

        }

        internal static string ModuleFolderPath { get { return Path.GetFullPath(moduleFolder); } }

        public static Dictionary<string,Type> ImportedTypes
        {
            get
            {
                return importedTypes;
            }
        }
    }
}
