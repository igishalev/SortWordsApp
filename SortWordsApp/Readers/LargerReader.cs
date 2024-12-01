using SortWordsApp.Container;
using SortWordsApp.Interfaces;

namespace SortWordsApp.Readers;

public class LargerReader : IFileReader
{
    private bool _isAscending;
    private WordContainer _container;

    public LargerReader(bool isAscending)
    {
        _isAscending = isAscending;
        _container = new WordContainer(isAscending);
    }

    public IWordContainer ReadFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            foreach (var word in line.ToLower()
                         .Split(new[] { ' ', '\r', '\n', ',', '.', '!', '?', ';', ':' },
                             StringSplitOptions.RemoveEmptyEntries))
            {
                _container.AddWord(word);
            }
        }

        return _container;
    }
}

