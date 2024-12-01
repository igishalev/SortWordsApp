using SortWordsApp.Container;
using SortWordsApp.Interfaces;

namespace SortWordsApp.Readers;

public class SmallReader : IFileReader
{
    private bool _isAscending;
    private WordContainer _container;

    public SmallReader(bool isAscending)
    {
        _isAscending = isAscending;
        _container = new WordContainer(isAscending);
    }

    public IWordContainer ReadFile(string filePath)
    {
        var words = File.ReadAllText(filePath).ToLower()
            .Split(new[] { ' ', '\r', '\n', ',', '.', '!', '?', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            _container.AddWord(word);
        }

        return _container;
    }
}