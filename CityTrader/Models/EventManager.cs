using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EventManager
    {
        private static EventManager instance;

        public static EventManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventManager();
                }

                return instance;
            }
        }

        public delegate void NPCEventHandler();
        public event NPCEventHandler OnRandomEncounter;

        public void RandomEncounter()
        {
            if (OnRandomEncounter != null)
            {
                OnRandomEncounter();
            }
        }
    }
}
