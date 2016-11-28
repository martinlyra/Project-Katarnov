using Katarnov.Module.Core.Mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katarnov.Events;
using Katarnov.Module.Core.Landmark;

namespace Katarnov.Module.Core
{
    public class CoreModule : IModule
    {
        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Version
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Entity GetPlayerEntity()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void OnClientAction()
        {
            throw new NotImplementedException();
        }

        public void OnClientConnect()
        {
            throw new NotImplementedException();
        }

        public void OnClientDisconnect()
        {
            throw new NotImplementedException();
        }

        public void OnClientReady()
        {
            throw new NotImplementedException();
        }

        public void OnReady()
        {
            throw new NotImplementedException();
        }

        public void OnWorldInitialize(object sender, WorldInitializeEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnWorldLoad()
        {
            throw new NotImplementedException();
        }

        public void OnWorldReady(object sender, WorldReadyEventArgs args)
        {
            var H = new PrimitiveHuman();

            LandmarkEntity L = EntityManager.GetInstancesOf<SpawnLandmark>().First();

            H.position = L.position;

            Global.gameInstance.playerCharacter = H;    
        }

        public Entity SpawnDefault()
        {
            return new PrimitiveHuman();
        }
    }
}
