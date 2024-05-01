using static LetterService;

class Program
{
    static void Main(string[] args) {
        List<string> date1 = getFolders("CombinedLetters/Input/Admission/");
        List<string> date2 = getFolders("CombinedLetters/Input/Scholarship/");

        List<string> dates = date1.Union(date2).ToList();

        foreach(string date in dates)
        {
            runOnDate(date.Substring(date.Length - 8));
        }
    }

    /// <summary>
    /// Run process on a date (archive, combine, report)
    /// </summary> 
    /// <param name="date">Date to look at and scan</param>
    static void runOnDate(string date)
    {
        List<string> admissionFiles = ScanFolder("CombinedLetters/Input/Admission/" + date);
        List<string> scholarshipFiles = ScanFolder("CombinedLetters/Input/Scholarship/" + date);

        LetterService letterService = new LetterService(admissionFiles, scholarshipFiles);

        // Set up paths
        CreateFolder("CombinedLetters/Archive/Admissions");
        CreateFolder("CombinedLetters/Archive/Scholarship");
        CreateFolderOverride("CombinedLetters/Output/" + date);
        CreateFolderOverride("CombinedLetters/Archive/Admissions/" + date);
        CreateFolderOverride("CombinedLetters/Archive/Scholarship/" + date);

        // Run Program
        string outputdate_path = "CombinedLetters/Output/" + date + "/";
        letterService.archiveFiles($"CombinedLetters/Archive/Admissions/{date}/", $"CombinedLetters/Archive/Scholarship/{date}/");
        letterService.Combine(outputdate_path);
        letterService.CreateReport(outputdate_path, date);
    }


    /// <summary>
    /// Scans a folder, retrieving all files and storing them as a list
    /// </summary> 
    /// <param name="folderLocation">Folder location to look for files</param>
    static List<string> ScanFolder(string folderLocation)
    {
        List<string> fileList = new List<string>();
        if (Directory.Exists(folderLocation))
        {
            // Retrieve all files in the directory
            string[] files = Directory.GetFiles(folderLocation);

            // Add each file to the list
            foreach (string file in files)
            {
                fileList.Add(file);
            }
        }
        else
        {
            Console.WriteLine("Directory does not exist for " + folderLocation);
            Console.WriteLine("This may be expected behavior if not every scholarship date has a matching admission, as the output will still be correct.");
        }

        return fileList;
    }


    /// <summary>
    /// Scans folder, retrieving all folders and storing them as a list.
    /// </summary> 
    /// <param name="folderLocation">Folder location to look for folders</param>
    static List<string> getFolders(string folderLocation)
    {
        List<string> folderList = new List<string>();
        if (Directory.Exists(folderLocation))
        {
            // Retrieve all folders in the directory
            string[] folders = Directory.GetDirectories(folderLocation);

            // Add each folder to the list
            foreach (string folder in folders)
            {
                folderList.Add(folder);
            }
        }
        else
        {
            Console.WriteLine("Warning: Directory does not exist: " + folderLocation);
        }
        return folderList;
    }

    /// <summary>
    /// Create a folder (if exists, delete)
    /// </summary> 
    /// <param name="folderName">Folder location to create (a full path)</param>
    static void CreateFolderOverride(string folderName)
    {
        if (Directory.Exists(folderName))
        {
            // If it exists, delete it and its contents
            Directory.Delete(folderName, true);
        }
        Directory.CreateDirectory(folderName);
    }

    /// <summary>
    /// Create a folder (if exists, do nothing)
    /// </summary> 
    /// <param name="folderName">Folder location to create (a full path)</param>
    static void CreateFolder(string folderName)
    {
        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(folderName);
        }
    }
}