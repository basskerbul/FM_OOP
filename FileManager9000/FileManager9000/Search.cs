interface ISearch
{
    /// <summary>
    /// Поиск
    /// </summary>
    /// <param name="substring"></param>
    string? TrySearch(string substring, string path);
}

internal class Search : ISearch
{
    private string area;

    internal string Area
    {
        get { return area; }
        set { area = value; }
    }

    public string? TrySearch(string substr, string area)
    {
        DirectoryInfo di = new DirectoryInfo(area);
        DirectoryInfo[] folders = di.GetDirectories();
        FileInfo[] files = di.GetFiles();
        string? result = null;

        foreach (DirectoryInfo folder in folders)
        {
            if(folder.Name[0] == '$')
                continue;

            foreach (FileInfo file in files)
            {
                if (substr.Contains(file.Name))
                {
                    return file.FullName;
                }
            }
            if (substr.Contains(folder.Name))
            {
                return folder.FullName;
            }
            else { result = TrySearch(substr, folder.FullName); }

        }
        return result;
    }
}