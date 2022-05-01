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
    /// Переносит папки/файлы
    /// </summary>
    /// <param name="path">куда перенести</param>
    /// <param name="name">что перенести</param>
    int Transfer(string path, string name);
}


class Functions : ICreate, IDelete, IRename, ICopyAndTransfer
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
    public int Transfer(string old_path, string new_path)
    {
        if (Directory.Exists(old_path))
        {
            Directory.CreateDirectory(new_path);
            Directory.Delete(old_path);
            return 0;
        }
        else if (File.Exists(old_path))
        {
            File.Create(new_path);
            File.Delete(old_path);
            return 0;
        }
        else
            return -1;
    }
}