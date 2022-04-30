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

string path = @"C:\";
int page = 0;

CommandsList cl = new CommandsList(path, page);
Interaction inter = new Interaction();
inter.OutputTree(path, page);
inter.InformationOutput(path);

while (true)
{
    cl.SimpleCommands(inter.Input());
}


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
    int Copy(string path_file, string path_copy);
    /// <summary>
    /// Копирует и переносит папки/файлы в новое место
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    void CopyAndTransfer(string path_file, string path_copy, string to_path);
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

interface ICommandRecognition
{
    /// <summary>
    /// Распознование простых команд пользователя
    /// </summary>
    /// <param name="command"></param>
    void SimpleCommands(string command);
    /// <summary>
    /// Распознование составных команд
    /// </summary>
    void ComplexCommands(string command);
}

class CommandsList: ICommandRecognition
{
    private Interaction interaction = new Interaction();
    private Functions functions = new Functions();
    private string path;
    private int page; 

    public CommandsList(string path, int page)
    {
        this.path = path;
        this.page = page;
    }

    public void SimpleCommands(string command)
    {
        switch (command)
        {
            case "next":
                Console.Clear();
                interaction.OutputTree(path, page++);
                interaction.InformationOutput(path);
                break;
            case "back":
                Console.Clear();
                interaction.OutputTree(path, page--);
                interaction.InformationOutput(path);
                break;
            case "help":
                Console.Clear();
                interaction.CommandWindow();
                break;
            default:
                ComplexCommands(command);
                break;
        }
    }
    public void ComplexCommands(string command)
    {
        string[] elements = command.Split(' ');
        if(elements[0] == "cd")
        {
            if(elements[1] == "..")
            {
                //Проверка на попытку выйти на несуществующий уровень
                if(path == @"C:\")
                {
                    path = @"C:\";
                    Console.Clear();
                    interaction.OutputTree(path, page);
                    interaction.InformationOutput(path);
                    interaction.MessageOutput("Выход на уровень выше невозможен");
                }
                else
                {
                    string[] path_elements = path.Split('\\');
                    page = 0;
                    string new_path = "";
                    for(int i = 0; i < path_elements.Length - 2; i++)
                    {
                        new_path += $@"{path_elements[i]}\";
                    }
                    Console.Clear();
                    interaction.OutputTree(new_path, page);
                    interaction.InformationOutput(new_path);
                    path = new_path;
                }
            }
            else
            {
                page = 0;
                string new_path = $@"{path}{elements[1]}\";
                Console.Clear();
                interaction.OutputTree(new_path, page);
                interaction.InformationOutput(new_path);
                path = new_path;
            }
        }
        else if(elements[0] == "info")
        {
            string file = "";
            for(int i = 1; i < elements.Length; i++)
            {
                file += $"{elements[i]} ";
            }
            string new_path = $@"{path}{file}";
            Console.Clear();
            interaction.OutputTree(path, page);
            interaction.InformationOutput(new_path);
        }
        else if(elements[0] == "copy")
        {
            string[] files = command.Split('"');
            string pathA = path + files[1];
            string pathB = path + files[3];
            if(elements[3] == "here")
            {
                functions.Copy(pathA, pathB);
                Console.Clear();
                interaction.OutputTree(path, page);
                interaction.InformationOutput(pathB);
            }
            else if(elements[1] == "to")
            {

            }
        }
    }
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
            DirectoryInfo folder = new DirectoryInfo(path);
            Console.WriteLine(format, "Information:");
            Console.WriteLine(format, $"{folder.Name}");
            Console.WriteLine(format, $"{folder.FullName}");
            Console.WriteLine(format, $"{folder.Attributes}");
            Console.WriteLine(format, $"{folder.CreationTime}");
            Console.WriteLine(down_frame);
            return 0;
        }
        else if (File.Exists(path))
        {
            FileInfo file = new FileInfo(path);
            Console.WriteLine(format, "Information:");
            Console.WriteLine(format, $"{file.Name}");
            Console.WriteLine(format, $"{file.FullName}");
            Console.WriteLine(format, $"{file.CreationTime}");
            Console.WriteLine(down_frame);
            return 0;
        }
        else
        {
            Console.WriteLine(up_frame);
            MessageOutput("Что-то пошло не так");
            Console.WriteLine(down_frame);
            return -1;
        }
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
        Console.WriteLine(format, "help");
        Console.WriteLine(down_frame);
    }
}

class Functions: ICreate, IDelete, IRename//, ICopyAndTransfer
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
    public int Copy(string path1, string path2)
    {
        if (Directory.Exists(path1))
        {
            Directory.CreateDirectory(path2);
            return 0;
        }
        else if (File.Exists(path1))
        {
            File.Copy(path1, path2);
            return 0;
        }
        else
            return -1;
    }
    public int CopyAndTransfer(string path_file, string path_copy, string to_path)
    {
        if (Directory.Exists(path_file))
        {
            Directory.Move()
            return 0;
        }
        else if (File.Exists(path_file))
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }
}