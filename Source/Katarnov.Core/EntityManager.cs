using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public class EntityManager
    {
        public readonly Dictionary<uint, Entity> entities = new Dictionary<uint, Entity>();


        readonly Queue<Entity> updateQueue = new Queue<Entity>();

        uint nextId = 0;
        readonly List<uint> freeIds = new List<uint>();

        readonly Game1 _game;

        public EntityManager(Game1 game)
        {
            _game = game;
        }

        public void QueryForUpdate()
        {
            updateQueue.Clear(); // just to make sure

            foreach (var o in entities.Values)
            {
                if (o.NeedsUpdate())
                    updateQueue.Enqueue(o);    
            }
        }

        public void ProcessUpdate()
        {
            while (updateQueue.Count > 0)
                updateQueue.Dequeue().Update();
        }

        public void Add(Entity e)
        {
            uint _id = nextId++;
            e.identifier.Assign(_id, e);
            entities.Add(_id, e);
        }

        internal void Reset()
        {
            updateQueue.Clear();
            entities.Clear();
            nextId = 0;
            freeIds.Clear();
        }
    }
}
