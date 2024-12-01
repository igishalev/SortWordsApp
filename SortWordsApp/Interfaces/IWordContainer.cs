namespace SortWordsApp.Interfaces;

public interface IWordContainer : IEnumerable<string>, IWordFrequency
{
    void AddWord(string word);
}