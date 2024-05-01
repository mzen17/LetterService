public interface ILetterService
{
    /// Reproduce all files from Input to Output
    void archiveFiles(string output_location);

    /// Run combine by using last 11 characters of file names
    void Combine(string output_location);

    /// Create the report and place it in the correct place
    /// Also puts details into analysis.
    void CreateReport(string report_location);
    
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
    }

    public void archiveFiles(string output_location) 
    {
        // Not implemented
    }

    public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile) 
    {
        // Not Implemented
    }

    public void Combine(string output_location) 
    {
        // Not Implemented
    }

    public void CreateReport(string report_location) 
    {
        // Not implemented
    }
}