using System.Collections;
using SortWordsApp.Interfaces;

namespace SortWordsApp.Container
{
    public class WordContainer : IWordContainer
    {
        private readonly SortedSet<string> _sortedSet;
        private readonly Dictionary<string, long> _wordCounts;
        private readonly Stack<KeyValuePair<string, long>> _maxStack;
        private string _mostFrequentWord;
        private long _maxOccurrences;

        public WordContainer(bool isAscending)
        {
            _wordCounts = new Dictionary<string, long>();
            _maxStack = new Stack<KeyValuePair<string, long>>();
            _sortedSet = isAscending
                ? new SortedSet<string>()
                : new SortedSet<string>(Comparer<string>.Create((x, y) => y.CompareTo(x)));
            _mostFrequentWord = null;
            _maxOccurrences = 0;
        }

        public void AddWord(string word)
        {
            if (!_wordCounts.ContainsKey(word))
            {
                _wordCounts[word] = 1;
                _sortedSet.Add(word);
            }
            else
            {
                _wordCounts[word]++;
            }

            if (_wordCounts[word] >= _maxOccurrences)
            {
                _maxOccurrences = _wordCounts[word];
                _maxStack.Push(new KeyValuePair<string, long>(word, _maxOccurrences));
            }
        }

        public List<KeyValuePair<string, long>> GetMaxOccurrences()
        {
            var maxOccurrences = new List<KeyValuePair<string, long>>();
            while (_maxStack.Count != 0 && _maxStack.Peek().Value == _maxOccurrences)
            {
                maxOccurrences.Add(_maxStack.Pop());
            }

            return maxOccurrences;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _sortedSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
