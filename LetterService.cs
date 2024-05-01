using System.IO;

public interface ILetterService
{
    /// <summary>
    /// Reproduce all files from Input to Output
    /// </summary> 
    /// <param name="output_location_admission">Admission Archive Output Location</param>
    /// <param name="output_location_scholarship">Scholarship Archive Output Location</param>
    void archiveFiles(string output_location_admission, string output_location_scholarship);

    /// <summary>
    /// Run combine by using last 11 characters of file names to detect if scholarship and admission belong to same student.
    /// </summary> 
    /// <param name="output_location">Output Location for combined letters</param>
    void Combine(string output_location);

    /// <summary>
    /// Create the report and place it in the correct place
    /// Also puts details into analysis.
    /// </summary> 
    /// <param name="report_location">Output Location for report</param>
    /// <param name="date">Date to print on report</param>
    void CreateReport(string report_location, string date) ;
    
    /// <summary>
    /// Combine two letter files into one file. 
    /// </summary> 
    /// <param name="inputFile1">File path for the first letter</param>
    /// <param name="inputFile2">File path for the first letter</param>
    /// <param name="resultFile">File path for the first letter</param>
    void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
}

public class LetterService : ILetterService
{
    private List<string> admissionFiles; // Variable for tracking list of admission files (file paths)
    private List<string> scholarshipFiles; // Variable for tracking list of scholarship files (file paths)

    private List<string> concated_letters; // Variable for tracking merged files

    /// Constructor that creates and initializes files
    public LetterService(List<string> af, List<string> sf) 
    {
        admissionFiles = af;
        scholarshipFiles = sf;
        concated_letters = new List<string>();
    }

    public void archiveFiles(string output_location_admission, string output_location_scholarship) 
    {
        // Move all admission files to specified place
        foreach (string file in admissionFiles) 
        {
            string fileCNT = File.ReadAllText(file);

            // Last 22 characters of file input will give correct file name.
            string correct_filename = file.Substring(file.Length - 22);
            File.WriteAllText(output_location_admission + correct_filename, fileCNT);
        }

        // Move all scholarship files to specified place
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
        List<string> merged_files = new List<string>();
        foreach(string file in admissionFiles) 
        {
            foreach(string file2 in scholarshipFiles) 
            {
                // This will only be true if the 8 digit University IDs are equal
                if (file.Substring(file.Length - 12) == file2.Substring(file2.Length - 12))
                {
                    // Last 12 letters (Substring(file.Length - 12)) gets filename without scholarship/admission
                    string newFileName = file.Substring(file.Length - 12);
                    CombineTwoLetters(file, file2, output_location + newFileName);
                    merged_files.Add(file.Substring(file.Length - 12));
                }
            }
        }
        concated_letters = merged_files;
    }

    public void CreateReport(string report_location, string date) 
    {
        // Parse Date
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);

        // Correct headers
        string reportHeader = $"{month}/{day}/{year} Report\n--------------------------------\n\n";
        string combineLetterHeader = $"Number of Combined Letters: {concated_letters.Count}\n";
        string formattedOutput = reportHeader + combineLetterHeader;

        // Go through list of tracked merged, and simply append them to file.
        foreach(string combine in concated_letters)
        {
            // ID is first 8 digit of [ID.txt]
            string id = combine.Substring(0, 8);
            formattedOutput += $"    {id}\n";
        }

        File.WriteAllText(report_location + "report.txt", formattedOutput);
    }
}