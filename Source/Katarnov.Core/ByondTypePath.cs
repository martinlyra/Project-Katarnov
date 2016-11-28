using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class ByondTypePath
    {
        string _fullPath;
        string _basePath;
        string _extension;

        public ByondTypePath(string @base, string full, string ext)
        {
            _fullPath = full;
            _basePath = @base;
            _extension = ext;
        }

        public string Full
        {
            get
            {
                return _fullPath;
            }
        }

        public string Base
        {
            get
            {
                return _basePath;
            }
        }

        public string Extension
        {
            get
            {
                return _extension;
            }
        }
    }
}
