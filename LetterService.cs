using System.IO;

public interface ILetterService
{
    /// Reproduce all files from Input to Output
    void archiveFiles(string output_location_admission, string output_location_scholarship);

    /// Run combine by using last 11 characters of file names
    void Combine(string output_location);

    /// Create the report and place it in the correct place
    /// Also puts details into analysis.
    void CreateReport(string report_location, string date) ;
    
    /// Combine two letter files into one file. 
    void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
}

public class LetterService : ILetterService
{
    private List<string> admissionFiles;
    private List<string> scholarshipFiles;

    private List<string> outputed;

    /// Constructor that creates and initializes files
    public LetterService(List<string> af, List<string> sf) 
    {
        admissionFiles = af;
        scholarshipFiles = sf;
        outputed = new List<string>();
    }

    public void archiveFiles(string output_location_admission, string output_location_scholarship) 
    {
        foreach (string file in admissionFiles) 
        {
            string fileCNT = File.ReadAllText(file);

            // Last 22 characters of file input will give correct file name.
            string correct_filename = file.Substring(file.Length - 22);
            File.WriteAllText(output_location_admission + correct_filename, fileCNT);
        }
        foreach (string file in scholarshipFiles)
        {
            string fileCNT = File.ReadAllText(file);

            // Same thing here, but with 24
            string correct_filename = file.Substring(file.Length - 24);
            File.WriteAllText(output_location_scholarship + correct_filename, fileCNT);
        }
    }

    public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile) 
    {
        string inputFile1Content = File.ReadAllText(inputFile1);
        string inputFile2Content = File.ReadAllText(inputFile2);
        
        File.WriteAllText(resultFile, inputFile1Content + inputFile2Content);
    }

    public void Combine(string output_location) 
    {
        List<string> merged = new List<string>();
        foreach(string file in admissionFiles) 
        {
            foreach(string file2 in scholarshipFiles) 
            {
                // This will only be true if the 8 digit University IDs are equal
                if (file.Substring(file.Length - 12) == file2.Substring(file2.Length - 12))
                {
                    string newFileName = file.Substring(file.Length - 12).Substring(0, 8) + "+" + file2.Substring(file2.Length - 12);
                    CombineTwoLetters(file, file2, output_location + newFileName);
                    merged.Add(file.Substring(file.Length - 12));
                }
            }
        }
        outputed = merged;
    }

    public void CreateReport(string report_location, string date) 
    {
        // Parse Date
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);

        string reportHeader = $"{month}/{day}/{year} Report\n--------------------------------\n\n";
        string combineLetterHeader = $"Number of Combined Letters: {outputed.Count}\n";

        string formattedOutput = reportHeader + combineLetterHeader;

        foreach(string combine in outputed)
        {
            // ID is first 8 digit of [ID.txt]
            string id = combine.Substring(0, 8);
            formattedOutput += $"    {id}\n";
        }

        File.WriteAllText(report_location + "report.txt", formattedOutput);
    }
}