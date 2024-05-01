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

    static void runOnDate(string date)
    {
        List<string> admissionFiles = ScanFolder("CombinedLetters/Input/Admission/" + date);
        List<string> scholarshipFiles = ScanFolder("CombinedLetters/Input/Scholarship/" + date);

        LetterService li = new LetterService(admissionFiles, scholarshipFiles);

        // Set up paths
        CreateFolder("CombinedLetters/Archive/Admissions");
        CreateFolder("CombinedLetters/Archive/Scholarship");
        CreateFolderOverride("CombinedLetters/Output/" + date);
        CreateFolderOverride("CombinedLetters/Archive/Admissions/" + date);
        CreateFolderOverride("CombinedLetters/Archive/Scholarship/" + date);

        // Run Program
        string outputdate_path = "CombinedLetters/Output/" + date + "/";
        li.archiveFiles($"CombinedLetters/Archive/Admissions/{date}/", $"CombinedLetters/Archive/Scholarship/{date}/");
        li.Combine(outputdate_path);
        li.CreateReport(outputdate_path, date);
    }

    /// Scans a folder, retrieving all files and storing them as a list
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

    /// Create a folder (if exists, delete)
    static void CreateFolderOverride(string folderName)
    {
        if (Directory.Exists(folderName))
        {
            // If it exists, delete it and its contents
            Directory.Delete(folderName, true);
        }
        Directory.CreateDirectory(folderName);
    }

    /// Create a folder (if exists, do nothing)
    static void CreateFolder(string folderName)
    {
        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(folderName);
        }
    }

    /// Scans folder, retrieving all folders and storing them as a list.
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
            Console.WriteLine("Directory does not exist: " + folderLocation);
        }

        return folderList;
    }
}