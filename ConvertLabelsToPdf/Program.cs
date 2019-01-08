using System;
using System.Linq;
using CommandLine;
using ConvertLabelsToPdf.CommandLineUtils;

namespace ConvertLabelsToPdf
{
    class Program
    {
        private static Options _options;
        private static string _parsingErrors;

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
        }

        private static void ShowOutput(string message = "")
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey(true);
        }

        private static bool ParseArguments(string[] args)
        {
            if (args == null || !args.Any())
            {
                return false;
            }

            ParserResult<Options> result = Parser
                .Default
                .ParseArguments<Options>(
                    args)
                .WithParsed(
                    options =>
                        _options = options)
                .WithNotParsed(errors =>
                    _parsingErrors = errors.ToString());

            return result != null && result.Tag == ParserResultType.Parsed;
        }
    }

    //by re.solved, re.solved.pl@gmail.com
}