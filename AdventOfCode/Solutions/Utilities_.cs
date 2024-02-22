namespace AdventOfCode.Solutions;

public class Utilities_
{
    protected StreamReader GetFileReaderByPath(string path)
    {
        // cr_mn: lecial blad
        // -> Unhandled exception. System.IO.FileNotFoundException: Could not find file '/Volumes/data/github/AdventOfCode2k23/AdventOfCode/input_1_2k23.txt'.
        // musialem zmienic na wyliczanie pelnej sciezki

        var filePath = GetFullPath(path);
        StreamReader streamReader = new StreamReader(filePath);
        return streamReader;
    }


    // cr_mn: tutaj nizej robie sztuczke z wyliczeniem nazwy folderu na podstawie nazwy klasy np "Day1" lub "Day2"

    private string GetFullPath(string path) =>
        Path.GetFullPath(Path.Join("Solutions/Solutions2k23", this.GetType().Name.TrimEnd('_'), path));

    // cr_mn: w .net mamy wygodne klasy File, Directory ze statycznymi metodami
    protected List<string> GetInputAsStringList(string path) => File.ReadAllLines(GetFullPath(path)).ToList();

    // protected List<string> GetInputAsStringList(string path)
    // {        
    //     StreamReader sr = GetFileReaderByPath(path);
    //     var lines = new List<string>();
    //     while (!sr.EndOfStream)
    //     {
    //         string line = sr.ReadLine() ?? throw new InvalidOperationException();
    //         lines.Add(line);
    //     }
    //     return lines;
    // }

    protected void measureTime()
    {
        throw new NotImplementedException();
    }

}