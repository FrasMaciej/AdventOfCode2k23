namespace AdventOfCode.Solutions;

public class Utilities
{
    protected StreamReader GetFileReaderByPath(string path)
    {
        string filePath = Path.GetFullPath(path);
        StreamReader streamReader = new StreamReader(filePath);
        return streamReader;
    }
    
    protected List<string> GetInputAsStringList(string path)
    {
        StreamReader sr = GetFileReaderByPath(path);
        var lines = new List<string>();
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? throw new InvalidOperationException();
            lines.Add(line);
        }
        return lines;
    }

    protected void measureTime()
    {
        throw new NotImplementedException();
    }

}