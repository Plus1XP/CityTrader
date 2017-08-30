using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenters
{
    public static class ResponsePresenter
    {

        public static string readString(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
            } while (result == "");
            return result;
        }

        public static int readInt(string prompt, int low, int high)
        {
            int result;
            do
            {
                string intString = readString(prompt);
                result = int.Parse(intString);
            } while ((result < low) || (result > high));
            return result;
        }
    }
}

public class Prompt
{
    public string stringResponse;
    public int intResponse;

    public bool hasCancelled;

    public string Message { get; private set; } = "";

    public void Response(string prompt, int low, int high, string exitmessage)
    {
        do
        {
            Console.WriteLine(prompt);
            stringResponse = Console.ReadLine();
            intResponse = int.Parse(stringResponse);
            ExitResponse(exitmessage);
        } while ((hasCancelled == true) && (stringResponse == "") && (intResponse < low) || (intResponse > high));
    }

    public void ExitResponse(string exitmessage)
    {
        if (intResponse == 0)
        {
            exitmessage = "";
            hasCancelled = false;
        }
    }
}
