using System.IO;
using System.Text.RegularExpressions;

namespace ConvertLabelsToPdf
{
    internal static class Validator
    {
        public static string ErrorMessage { get; private set; }

        public static bool ValidateParameters(string sourceDirectoryPath, string targetDirectoryPath, string targetPdfFileName)
        {
            if (!string.IsNullOrEmpty(sourceDirectoryPath))
            {
                if (!DirectoryExists(sourceDirectoryPath))
                {
                    ErrorMessage += $"Source directory path: [{sourceDirectoryPath}], does not exist. \n";

                    return false;
                }

                if (Directory.GetFiles(sourceDirectoryPath).Length == 0)
                {
                    ErrorMessage += $"Source directory path: [{sourceDirectoryPath}], contains no files. \n";
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(targetDirectoryPath) && !DirectoryExists(targetDirectoryPath))
            {
                ErrorMessage +=  $"Target directory path:[{targetDirectoryPath}], does not exist. \n";

                return false;
            }

            if (string.IsNullOrEmpty(targetPdfFileName))
            {
                return true;
            }

            const string illegalChars = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";

            bool isValidName = Regex.IsMatch(targetPdfFileName, illegalChars, RegexOptions.CultureInvariant);

            if (!isValidName)
            {
                ErrorMessage = $"Target pdf file name value: [{targetPdfFileName}], is not valid for file name.";
            }

            return isValidName;

        }

        private static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}