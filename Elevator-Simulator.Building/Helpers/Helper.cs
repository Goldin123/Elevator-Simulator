
using Microsoft.Extensions.Logging;

namespace Elevator_Simulator.Building.Helpers
{
    public static class Helper
    {
        public static int GetIntInput(ILogger logger,string prompt)
        {
            logger.LogInformation(string.Format("{0} {1}", DateTime.Now,$"\n{prompt}"));
            int userInput;
            while (!(int.TryParse(Console.ReadLine(), out userInput) && userInput > 0))
            {
                logger.LogWarning($"Please enter a positive number greater than zero.\n{prompt}");
            }
            return userInput;
        }
        public static int GetIntInput(ILogger logger, string prompt, int maximum)
        {
            logger.LogInformation(string.Format("{0} {1}", DateTime.Now, $"\n{prompt}"));
            int userInput;
            while (!(int.TryParse(Console.ReadLine(), out userInput) && userInput > 0 && userInput <= maximum))
            {
                logger.LogWarning($"Please enter a positive number which is between 1 and {maximum}.\n{prompt}");
            }
            return userInput;
        }
        public static int GetIntInput(ILogger logger, string prompt, int maximum, int current)
        {
            logger.LogInformation(string.Format("{0} {1}", DateTime.Now, $"\n{prompt}"));
            int userInput;
            while (!(int.TryParse(Console.ReadLine(), out userInput) && userInput > 0 && userInput <= maximum && userInput != current))
            {
                logger.LogWarning($"Please enter a positive number which is between 1 and {maximum} and should not be {current}.\n{prompt}");
            }
            return userInput;
        }

    }
}
