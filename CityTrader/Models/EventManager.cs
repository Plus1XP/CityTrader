namespace Models
{
    public class EventManager
    {
        private static EventManager instance;

        public delegate void NPCEventHandler();

        public event NPCEventHandler OnRandomEncounter;

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

        public void RandomEncounter()
        {
            if (this.OnRandomEncounter != null)
            {
                this.OnRandomEncounter();
            }
        }
    }
}
