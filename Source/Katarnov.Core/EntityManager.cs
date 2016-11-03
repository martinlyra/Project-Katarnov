using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katarnov
{
    public static class EntityManager
    {
        public static readonly Dictionary<uint, Entity> entities = new Dictionary<uint, Entity>();

        static readonly Queue<Entity> updateQueue = new Queue<Entity>();

        static uint nextId = 0;
        static readonly List<uint> freeIds = new List<uint>();

        static Game1 _game;

        internal static void Initialize(Game1 game)
        {
            _game = game;
        }

        public static void PopulateEvents()
        {

        }

        public static void QueryForUpdate()
        {
            updateQueue.Clear(); // just to make sure

            foreach (var o in entities.Values)
            {
                if (o.NeedsUpdate())
                    updateQueue.Enqueue(o);    
            }
        }

        public static void ProcessUpdate()
        {
            while (updateQueue.Count > 0)
                updateQueue.Dequeue().Update();
        }

        public static void Add(Entity e)
        {
            uint _id = nextId++;
            e.identifier.Assign(_id, e);
            entities.Add(_id, e);
        }

        internal static void Reset()
        {
            updateQueue.Clear();
            entities.Clear();
            nextId = 0;
            freeIds.Clear();
        }
    }
}
