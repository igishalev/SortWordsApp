namespace SortWordsApp.Interfaces;

public interface IProcessor
{
    List<KeyValuePair<string, long>> Process(string source, string destination);
}