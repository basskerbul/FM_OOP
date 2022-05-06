interface IOutput
{
    /// <summary>
    /// Вывод дерева директорий
    /// </summary>
    /// <param name="values"></param>
    /// <param name="format"></param>
    void OutputTree(string path, int page);
    /// <summary>
    /// Вывод информации о папке/файле
    /// </summary>
    /// <param name="values"></param>
    int InformationOutput(string path);
    /// <summary>
    /// Вывод информационного сообщения
    /// </summary>
    /// <param name="message"></param>
    void MessageOutput(string message);
    /// <summary>
    /// Вывод списка команд
    /// </summary>
    void CommandWindow();
}
interface IInput
{
    /// <summary>
    /// Ввод
    /// </summary>
    /// <returns></returns>
    string Input();
}
class Interaction: IOutput, IInput
{
    static private int page_size = 6;
    static private string format = "║{0, -56}║";
    static private string up_frame = "╔════════════════════════════════════════════════════════╗";
    static private string middle_frame = "╠════════════════════════════════════════════════════════╣";
    static private string down_frame = "╚════════════════════════════════════════════════════════╝";

    public string Input()
    {
        return Console.ReadLine();
    }

    public void OutputTree(string path, int page)
    {
        string[] folders = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        if (page < 0)
            page = 0;

        Console.WriteLine(up_frame);
        Console.WriteLine(format, path);
        foreach (string file in files)
        {
            string[] file_name = file.Split('\\');
            Console.WriteLine(format, $"     {file_name[file_name.Length - 1]}");
        }
        Console.WriteLine(format, "");
        for (int i = page * page_size; i < page * page_size + page_size; i++)
        {
            if (folders.Length <= i)
                break;
            string[] folder_name = folders[i].Split('\\');
            Console.WriteLine(format, folder_name[folder_name.Length - 1]);
        }
        Console.WriteLine(middle_frame);
    }
    public int InformationOutput(string path)
    {
        if (Directory.Exists(path))
        {
            CalculateSize calculateSize = new CalculateSize(path);

            DirectoryInfo folder = new DirectoryInfo(path);

            Console.WriteLine(format, "Information:");
            Console.WriteLine(format, $"{folder.Name}");
            Console.WriteLine(format, $"{folder.FullName}");
            Console.WriteLine(format, $"{folder.Attributes}");
            Console.WriteLine(format, $"{folder.CreationTime}");
            if(path != @"C:\")
                Console.WriteLine(format, $"{calculateSize.Size(path)} bytes");
            Console.WriteLine(down_frame);
            return 0;
        }
        else if (File.Exists(path))
        {
            CalculateSize calculateSize = new CalculateSize(path);

            FileInfo file = new FileInfo(path);

            Console.WriteLine(format, "Information:");
            Console.WriteLine(format, $"{file.Name}");
            Console.WriteLine(format, $"{file.FullName}");
            Console.WriteLine(format, $"{file.CreationTime}");
            Console.WriteLine(format, $"{calculateSize.Size(path)}");
            Console.WriteLine(down_frame);
            return 0;
        }
        else
            return -1;
    }
    public void MessageOutput(string message)
    {
        Console.WriteLine(up_frame);

        Console.WriteLine(format, message);
        Console.WriteLine(down_frame);
    }
    public void CommandWindow()
    {
        Console.WriteLine(up_frame);
        Console.WriteLine(format, "cd ..");
        Console.WriteLine(format, "cd");
        Console.WriteLine(format, "next");
        Console.WriteLine(format, "back");
        Console.WriteLine(format, "copy \"file1\" \"file2\" here");
        Console.WriteLine(format, "copy \"file1\" \"file2\" to \"path\"");
        Console.WriteLine(format, "transfer \"name\" to \"path\"");
        Console.WriteLine(format, "find \"folder/file\"");
        Console.WriteLine(format, "help");
        Console.WriteLine(down_frame);
    }
    public void SearchWindow(string request, string result)
    {
        Console.WriteLine(up_frame);
        Console.WriteLine(format, $"По запросу {request} был найден путь:");
        Console.WriteLine(format, result);
        Console.WriteLine(down_frame);
    }
}
