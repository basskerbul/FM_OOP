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
        string? result = null;
        try
        {
            DirectoryInfo folders = new DirectoryInfo(area);
            foreach (DirectoryInfo folder in folders.GetDirectories())
            {
                DirectoryInfo relative_path = new DirectoryInfo(Path.GetFullPath($@"C:{substr}"));
                if (relative_path.Exists)
                    return relative_path.FullName;
                else
                {
                    return TrySearch(substr, folder.FullName);
                }
            }
            return result;
        }
        catch { return null; }
    }
}