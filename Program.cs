using static LetterService;

class Program
{
    static void Main(string[] args)
    {
        string rootFolder = args[0];
        string date = "text";

        List<string> admissionFiles = scanFolder("CombinedLetters");
        List<string> scholarshipFiles = scanFolder("CombinedLetters");

        LetterService li = new LetterService(admissionFiles, scholarshipFiles);

        string outputdate_path = rootFolder + date;
        li.archiveFiles(outputdate_path);
        li.Combine(outputdate_path);
        li.CreateReport(outputdate_path);
    }

    /// Scans a folder, retrieving all files and storing them as a list
    static List<string> scanFolder(string folderLocation) {
        List<string> fileList = new List<string>();
        /// Not implemented yet
        return fileList;
    }
}