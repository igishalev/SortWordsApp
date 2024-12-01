namespace SortWordsApp.Interfaces;

public interface IWordFrequency
{
    public List<KeyValuePair<string, long>> GetMaxOccurrences();
}