namespace SortWordsApp.Interfaces;

public interface IFileWriter
{
    void WriteFile(string path, IEnumerable<string> data);
}