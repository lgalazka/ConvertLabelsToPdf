using System;
using CommandLine;
using ConvertLabelsToPdf.CommandLineUtils;

namespace ConvertLabelsToPdf
{
    class Program
    {
        private static Options _options;

        static void Main(string[] args)
        {
            if (!ParseArguments(args))
            {
                ShowOutput();
                return;
            }

            string sourceDirectoryPath = _options.SourceDirectory;
            string targetDirectoryPath = _options.TargetDirectory;
            string targetPdfFileName = _options.TargetPdfFileName;

            if (!Validator.ValidateParameters(sourceDirectoryPath, targetDirectoryPath, targetPdfFileName))
            {
                ShowOutput(Validator.ErrorMessage);
                return;
            }

            Converter.GeneratePdfFromSourceImages(sourceDirectoryPath, targetDirectoryPath, targetPdfFileName);

            ShowOutput(Converter.IsConvertedSuccessfully ? "Conversion to pdf finished successfully!\n" : Converter.ErrorMessage);
        }

        private static void ShowOutput(string message = "")
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey(true);
        }

        private static bool ParseArguments(string[] args)
        {
            ParserResult<Options> result = Parser
                .Default
                .ParseArguments<Options>(
                    args)
                .WithParsed(
                    options =>
                        _options = options);

            return result != null && result.Tag == ParserResultType.Parsed;
        }
    }

    //by re.solved, re.solved.pl@gmail.com
}