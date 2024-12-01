namespace SortWordsApp.Interfaces;

public interface IFileReader
{
    IWordContainer ReadFile(string filePath);
}