using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    class PythonManager
    {
        readonly ScriptEngine pythonEngine = Python.CreateEngine();

        const string scriptDirectory = "Katarnov";

        List<string> searchPaths = new List<string>()
        {
            Path.GetFullPath(scriptDirectory)
        };

        PythonManager()
        {
            pythonEngine.SetSearchPaths(searchPaths);
        }
    }
}
