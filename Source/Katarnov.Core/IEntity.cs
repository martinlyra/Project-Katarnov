using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module
{
    public interface IEntity 
    {
        void Initialize();

        void Update();
        void Draw();

        bool NeedsUpdate();
        bool ShouldDraw();
    }
}
