using System;
using CommandLine;
using ConvertLabelsToPdf.CommandLineUtils;

namespace ConvertLabelsToPdf
{
    internal static class Program
    {
        private static Options _options;
        private static ConsoleColor _errorColor = ConsoleColor.Red;
        private static ConsoleColor _successColor = ConsoleColor.Green;

        private static void Main(string[] args)
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
                ShowOutput(Validator.ErrorMessage, _errorColor);
                return;
            }

            Converter.GeneratePdfFromSourceImages(sourceDirectoryPath, targetDirectoryPath, targetPdfFileName);

            ShowOutput(Converter.IsConvertedSuccessfully ? "Conversion to pdf finished successfully!\n" : Converter.ErrorMessage, _successColor);
        }

        private static void ShowOutput(string message = "", ConsoleColor? color = null)
        {
            if (color != null)
            {
                Console.ForegroundColor = (ConsoleColor)color;
            }

            Console.WriteLine(message);
            Console.ResetColor();
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

    //by (lga) lukagalazka@gmail.com
}