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

        public delegate void ExpereienceEventHandler(long amount);
        public event ExpereienceEventHandler OnExperiencedGained;

        public void GainExperience(long amount)
        {
            if (OnExperiencedGained != null)
            {
                OnExperiencedGained(amount);
            }
        }
    }
}
