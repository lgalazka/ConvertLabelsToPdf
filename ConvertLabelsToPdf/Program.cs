using System;
using System.Linq;

namespace ConvertLabelsToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ParseArguments(args))
            {
                return;
            }

            Console.WriteLine("Program invoked without any or with invalid parameters!");
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey(true);
        }

        private static bool ParseArguments(string[] args)
        {
            if (args == null || !args.Any())
            {
                return false;
            }

            var parseResult = false;

            return parseResult;
        }
    }

    //by re.solved, re.solved.pl@gmail.com
}