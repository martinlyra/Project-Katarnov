using Katarnov.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov.Module
{
    public interface IModule
    {
        string Name { get; }
        string Description { get; }
        string Version { get; }

        void Initialize();
        void OnReady();

        void OnClientConnect();
        void OnClientReady();
        void OnClientDisconnect();

        void OnClientAction();

        void OnWorldLoad();
        void OnWorldInitialize(object sender, WorldInitializeEventArgs args);
        void OnWorldReady(object sender, WorldReadyEventArgs args);

        Entity GetPlayerEntity();

        Entity SpawnDefault();
    }
}
