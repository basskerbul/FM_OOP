//### Файловый менеджер должен иметь возможность:
//*показывать содержимое дисков;
//*создавать папки / файлы; +
//*удалять папки / файлы; +
//*переименовывать папки / файлы; +
//*копировать / переносить папки / файлы; +-
//*вычислять размер папки/файла;
//*производить поиск по маске (с поиском по подпапкам);
//*для текстовых файлов готовить статические данные (кол-во слов, 
//кол-во строк, кол-во абзацев, кол-во символов с пробелами, кол-во слов без пробелов).

interface ICreate
{
    /// <summary>
    /// Создание папки/файла
    /// </summary>
    int Create(string path, string name);
}
interface IDelete
{
    /// <summary>
    /// Удаление попки/файла
    /// </summary>
    /// <param name="path"></param>
    int Delete(string path);
}
interface IRename
{
    /// <summary>
    /// Переименовывает папки/файлы
    /// </summary>
    /// <param name="path"></param>
    int Rename(string path, string NewName);
}
interface ICopyAndTransfer
{
    /// <summary>
    /// Копирует папки/файлы в том же месте
    /// </summary>
    /// <param name="path"></param>
    int Copy(string path, string name);
    /// <summary>
    /// Копирует и переносит папки/файлы в новое место
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    void CopyAndTransfer(string path, string name);
    /// <summary>
    /// Переносит папки/файлы
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    void Transfer(string path, string name);
}

interface IOutput
{
    /// <summary>
    /// Вывод дерева директорий
    /// </summary>
    /// <param name="values"></param>
    /// <param name="format"></param>
    void OutputTree(string[] folders, string[] files);
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
}
interface IInput
{
    /// <summary>
    /// Ввод
    /// </summary>
    /// <returns></returns>
    string Input();
}

class Interaction: IOutput
{
    static private string format = "║{0, -56}║";
    static private string up_frame = "╔════════════════════════════════════════════════════════╗";
    static private string down_frame = "╚════════════════════════════════════════════════════════╝";

    public void OutputTree(string[] folders, string[] files)
    {
        Console.WriteLine(up_frame);
        foreach(string folder in folders)
        {
            Console.WriteLine(format, folder);
        }
        foreach(string file in files)
        {
            Console.WriteLine(format, file);
        }
        Console.WriteLine();
    }
    public int InformationOutput(string path)
    {
        if (Directory.Exists(path))
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            DirectoryInfo folder = new DirectoryInfo(path);
            Console.WriteLine(format, $"{folder.Name}");
            Console.WriteLine(format, $"{folder.FullName}");
            Console.WriteLine(format, $"{folder.Attributes}");
            Console.WriteLine(format, $"{folder.CreationTime}");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            return 0;
        }
        else if (File.Exists(path))
        {
            FileInfo file = new FileInfo(path);
            Console.WriteLine(format, $"{file.Name}");
            Console.WriteLine(format, $"{file.FullName}");
            Console.WriteLine(format, $"{file.CreationTime}");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            return 0;
        }
        else
        {
            MessageOutput("Что-то пошло не так");
            return -1;
        }
    }
    public void MessageOutput(string message)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine(format, message);
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    }
}

class Functions: ICreate, IDelete, IRename, ICopyAndTransfer
{
    public int Create(string path, string name)
    {
        if (Directory.Exists(path))
        {
            Directory.CreateDirectory($@"{path}\{name}");
            return 0;
        }
        else if (File.Exists(path))
        {
            File.Create($@"{path}\{name}");
            return 0;
        }
        else
            return -1;
    }
    public int Delete(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
            return 0;
        }
        else if (File.Exists(path))
        {
            File.Delete(path);
            return 0;
        }
        else
            return -1;
    }
    public int Rename(string path, string NewName)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
            string[] folders = path.Split('\\');
            path = "";
            for(int i = 0; i < folders.Length - 1; i++)
            {
                path += $@"{folders[i]}\";
            }
            Directory.CreateDirectory($@"{path}\{NewName}");
            return 0;
        }
        else if (File.Exists(path))
        {
            File.Delete(path);
            string[] folders = path.Split('\\');
            path = "";
            for(int i = 0; i < folders.Length - 1; i++)
            {
                path += $@"{folders[i]}\";
            }
            File.Create($@"{path}\{NewName}");
            return 0;
        }
        else
            return -1;
    }
    public int Copy(string path, string NewName)
    {
        if (Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            return 0;
        }
        else if (File.Exists(path))
        {
            File.Create($@"{path}\{name}");
            return 0;
        }
        else
            return -1;
    }
}