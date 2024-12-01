using SortWordsApp.Container;
using SortWordsApp.Interfaces;

namespace SortWordsApp.Readers;

public class MediumReader : IFileReader
{
    private WordContainer _container;

    public MediumReader(bool isAscending)
    {
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

