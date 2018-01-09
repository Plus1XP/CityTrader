using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class NPCModel
    {
        public string Name { get; set; }
        public int LowPercentage { get; set; }
        public int HighPercentage { get; set; }
        public string LowMessage { get; set; }
        public string HighMessage { get; set; }
        public string EncounterMessage { get; set; }

        public abstract string Cooperate();
        public abstract string CooperationMessage(decimal charge);
        public abstract string ReceivePenalty();
        public abstract string PenaltyMessage(decimal penalty);

        public NPCModel()
        {

        }

        public string Encounter()
        {
            return EncounterMessage;
        }

        public string PlayerInteraction(string response)
        {
            string Message = string.Empty;
            if (response.Equals("y") || response.Equals("yes"))
            {
                Message = Cooperate();
            }
            else if (response.Equals("n") || response.Equals("no"))
            {
                Message = TryToEscape();
            }            
            else
            {
                Message = "Invalid Response";
                //Encounter();
            }
            return Message;
        }

        public string TryToEscape()
        {
            string Message = string.Empty;
            int Outcome = RNGModel.RandomNumber.Next(0, 2);

            if (Outcome == 0)
            {
                Message = EscapeMessage();
            }
            else if (Outcome == 1)
            {
                Message = ReceivePenalty();
            }
            return Message;
        }

        public string EscapeMessage()
        {
            string Message = "You got away";
            return Message;
        }
    }
}