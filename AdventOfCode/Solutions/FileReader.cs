namespace AdventOfCode.Solutions;

public class FileReader
{
    protected StreamReader GetFileReaderByPath(string path)
    {
        string filePath = Path.GetFullPath(path);
        StreamReader streamReader = new StreamReader(filePath);
        return streamReader;
    }
}