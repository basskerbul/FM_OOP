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
    /// Вывод информации на экран
    /// </summary>
    /// <param name="values"></param>
    /// <param name="format"></param>
    void Output(string[] values, string format);
}
interface IInput
{
    /// <summary>
    /// Ввод
    /// </summary>
    /// <returns></returns>
    string Input();
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