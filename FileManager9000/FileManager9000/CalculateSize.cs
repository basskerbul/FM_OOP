
interface IReturnSize
{
    /// <summary>
    /// Вычисляет размер файла/папки
    /// </summary>
    /// <returns></returns>
    long Size(string path);
}
internal class CalculateSize: IReturnSize
{
    private string path;
    
    public CalculateSize(string path)
    {
        this.path = path;
    }

    public long Size(string path)
    {
        if (Directory.Exists(path))
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            long result = GetSizeDirectory(directoryInfo);
            return result;
        }
        else if (File.Exists(path))
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }
        return -1;
    }
    private long GetSizeDirectory(DirectoryInfo di)
    {
        long size = 0;
        FileInfo[] files = di.GetFiles();
        foreach(FileInfo file in files)
        {
            size += file.Length; 
        }
        DirectoryInfo[] folders = di.GetDirectories();
        foreach(DirectoryInfo folder in folders)
        {
            size += GetSizeDirectory(folder);
        }
        return size;
    }
}