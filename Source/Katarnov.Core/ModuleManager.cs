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
        static readonly Dictionary<string, string> inactiveModuleList = 
            new Dictionary<string, string>();
        static readonly Dictionary<string, KeyValuePair<ModuleInfo, Assembly>> moduleList = 
            new Dictionary<string, KeyValuePair<ModuleInfo, Assembly>>();

        static readonly Dictionary<string, Type> importedTypes =
            new Dictionary<string, Type>(); 

        static readonly string moduleFolder = "Modules";
        static readonly string developmentModuleFolder = "../../Modules";

        internal static void Scan()
        {
            if (!Directory.Exists(moduleFolder))
                Directory.CreateDirectory(moduleFolder);

            List<string> assemblies = Directory.GetFiles(moduleFolder).ToList();

# if (DEBUG)
            foreach (var m in Directory.GetFiles(developmentModuleFolder))
                assemblies.Add(m);
# endif
            assemblies = assemblies.Where(a =>
           {
               return Path.GetExtension(a) == ".dll";
           }).ToList();

            Console.WriteLine(assemblies.Count);

            //var appDomain = AppDomain.CreateDomain("ModuleScan");
            foreach (var a in assemblies)
            {
                var dllName = Path.GetFileNameWithoutExtension(a);

                if (!dllName.Contains("Module."))
                    continue;

                var assembly = Assembly.LoadFrom(a);
                if ( IsGameModule(assembly) )
                    moduleList.Add(assembly.FullName, 
                        new KeyValuePair<ModuleInfo,Assembly>(
                            ModuleInfo.CreateFrom(
                                assembly.GetImplementationsOf<IModule>().First()),
                            assembly));
                    //inactiveModuleList.Add(assembly.FullName, a);
            }
            //AppDomain.Unload(appDomain);
        }

        internal static bool IsGameModule(Assembly ass)
        {
            foreach (var t in ass.DefinedTypes)
                if (t.Implements<IModule>())
                    return true;
            return false;
        }

        internal static void Initialize()
        {
            Scan();

            LoadModules();
            InitializeModules();
        }

        internal static void LoadModules()
        {
            foreach (var ass in moduleList.Values)
            {
                try
                {
                    Assembly assembly = ass.Value;

                    Console.WriteLine(assembly.GetName().Name);

                    foreach (var t in assembly.GetExportedTypes())
                    {
                        /*if (t.BaseType == typeof(EntityDefine))
                        {
                            var ed = (EntityDefine)Activator.CreateInstance(t);

                            Global.gameInstance.entityDatabase.internalDatabase.Add(t.Name, ed);
                            Console.WriteLine("-> Imported {0}", t.Name, 
                                assembly.GetName().Name);
                        }*/
                        if (t.Inherits<Entity>())
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("IMPORT: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("{0} :", t.Name);
                            Console.ResetColor();
                            t.GetTypeInfo().GetInheritedBaseTypes().ToList().ForEach(
                                o => Console.Write(" -> {0}", o));
                            Console.Write("\n");
                            importedTypes.Add(t.Name, t);
                            Global.gameInstance.entityDatabase.AddEntityType(t);
                        }
                        else
                        {
                            Console.WriteLine("Skipping {0}", t.Name);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        internal static void InitializeModules()
        {
            foreach (IModule mod in moduleList.Values.Select(kvp => kvp.Key.Interface))
            {
                World.OnInitialize += mod.OnWorldInitialize;
                World.OnReady += mod.OnWorldReady;   
            }
        }

        internal static string ModuleFolderPath { get { return Path.GetFullPath(moduleFolder); } }

        public static Dictionary<string,Type> ImportedTypes
        {
            get
            {
                return importedTypes;
            }
        }

        public static ModuleInfo FirstModule
        {
            get
            {
                return moduleList.First().Value.Key;
            }
        }

        public static Dictionary<string, KeyValuePair<ModuleInfo,Assembly>> Modules
        {
            get
            {
                return moduleList;
            }
        }
    }
}
