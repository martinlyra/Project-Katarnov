using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IronPython;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.CSharp;
using IronPython.Hosting;
using IronPython.Runtime.Types;
using System.Runtime.Remoting;
using IronPython.Runtime;
using System.IO;
using Katarnov.Module;

namespace Katarnov
{
    internal class EntityDatabase
    {
        internal readonly Dictionary<string, Type> typeDatabase = new Dictionary<string, Type>();  
        internal readonly Dictionary<string, EntityDefine> internalDatabase = new Dictionary<string, EntityDefine>();

        ScriptEngine pythonEngine;

        readonly string baseScriptDirectory = Path.GetFullPath("Katarnov");

        readonly Game1 _game;

        public EntityDatabase(Game1 game)
        {
            _game = game;

            pythonEngine = Python.CreateEngine();

            var list = pythonEngine.GetSearchPaths();
            list.Add(baseScriptDirectory);

            pythonEngine.SetSearchPaths(list);
        }

        public void Initialize()
        {

        }

        public bool AddEntityType<T>() where T : Entity
        {
            var t = typeof(T);
            typeDatabase.Add(t.Name,t);
            return true;
        }

        public Type GetEntityTypeByName(string name)
        {
            if (typeDatabase.ContainsKey(name))
                return typeDatabase[name].AsType();
            return null;
        }

        public void InitializePython()
        {
            ScriptSource source = pythonEngine.CreateScriptSourceFromFile("Katarnov/PyKatarnov/include.py");
            CompiledCode ccode = source.Compile();
            ScriptScope scope = ccode.DefaultScope;  // pythonEngine.CreateScope();
            ObjectOperations op = pythonEngine.Operations;

            try
            {
                foreach (var o in scope.GetItems().ToList())
                {
                    Console.WriteLine(o);
                    if (o.Value is PythonModule)
                    {
                        scope.ImportModule(o.Key);
                    }
                }

                source.Execute(scope);

                foreach (var o in scope.GetItems().ToList())
                {
                    if (o.Value is PythonType)
                    {
                        try
                        {
                            string name = o.Key;

                            EntityDefine entdef = new EntityDefine();

                            object defClass = scope.GetVariable(name);
                            object spriteDef;

                            op.TryGetMember(defClass, "sprite", out spriteDef);

                            entdef.typeName = name;
                            entdef.spriteDef = spriteDef.ToString();

                            internalDatabase.Add(name, entdef);
                            Console.WriteLine("Define for \"{0}\" added to database");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Finished initializing the database");
        }

        public EntityDefine GetDefine(string defineName)
        {
            return internalDatabase[defineName];
        }
    }
}
