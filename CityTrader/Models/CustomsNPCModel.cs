using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //public class CustomsAgent : NPCModel
    //{
    //    public CustomsAgent() : base()
    //    {
    //        this.Name = "TSA";
    //        this.LowPercentage = 20;
    //        this.HighPercentage = 50;
    //        this.EncounterMessage = "Do you have anything to declare? \n(Y)es or (N)o?";
    //    }

    //    public override string Cooperate()
    //    {
    //        string message = string.Empty;
    //        decimal customsCharge = CalculateCooperationPercentage();
    //        PlayerModel.Instance.Money -= customsCharge;
    //        message = CooperationMessage(customsCharge);
    //        return message;
    //    }

    //    public decimal CalculateCooperationPercentage()
    //    {
    //        decimal customsPercenatge = (decimal)LowPercentage / 100;
    //        decimal customCharge = PlayerModel.Instance.Money * customsPercenatge;
    //        return customCharge;
    //    }

    //    public override string CooperationMessage(decimal charge)
    //    {
    //        string Message = $"Thank you for your cooperation! \nYou have been charged our lowest rate {charge:C}";
    //        return Message;
    //    }

    //    public override string ReceivePenalty()
    //    {
    //        string Message = string.Empty;
    //        decimal finalPenalty = CalculatePenaltyPercentage();
    //        PlayerModel.Instance.Money -= finalPenalty;
    //        Message = PenaltyMessage(finalPenalty);
    //        return Message;
    //    }

    //    public decimal CalculatePenaltyPercentage()
    //    {
    //        int randomPenalty = RNGModel.RandomNumber.Next(LowPercentage + 5, HighPercentage + 1);
    //        //Either cast the equation to a double or use.0 after the int to force it as a double. 
    //        decimal penaltyPercentage = (decimal)randomPenalty / 100;
    //        decimal penaltyCharge = PlayerModel.Instance.Money * penaltyPercentage;
    //        return penaltyCharge;
    //    }

    //    public override string PenaltyMessage(decimal penalty)
    //    {
    //        string Message = $"You have been randomly searched. You have not declared your items! \nYou have been fined {penalty:C}";
    //        return Message;
    //    }
    //}
}
