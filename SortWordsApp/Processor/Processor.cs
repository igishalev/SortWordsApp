using SortWordsApp.Interfaces;

namespace SortWordsApp.Processor;

public class Processor : IProcessor
{
    public bool IsAscending { get; set; }
    private readonly IFileReader _reader;
    private readonly IFileWriter _writer;
    
    public Processor(IFileReader reader, IFileWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public List<KeyValuePair<string, long>> Process(string inputFilePath, string outputFilePath)
    {
        var words = _reader.ReadFile(inputFilePath);
        _writer.WriteFile(outputFilePath, words);

        return words.GetMaxOccurrences();
    }
}