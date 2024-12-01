using SortWordsApp.Interfaces;
using SortWordsApp.Readers;
using SortWordsApp.Writers;

namespace SortWordsApp.Processor;

public class ProcessorStrategy
{
    private bool _isAscending { get; set; }

    public ProcessorStrategy(bool isAscending)
    {
        _isAscending = isAscending;
    }

    public Processor LargeFileProcessor()
    {
        var reader = new LargerReader(_isAscending);
        var writer = new LargerWriter();
        return new Processor(reader, writer);
    }

    public Processor MediumFileProcessor()
    {
        var reader = new MediumReader(_isAscending);
        var writer = new MediumWriter();
        return new Processor(reader, writer);
    }

    public Processor SmallFileProcessor()
    {
        var reader = new SmallReader(_isAscending);
        var writer = new SmallWriter();
        return new Processor(reader, writer);
    }

    public IProcessor GetFileProcessor(FileSize fileSize)
    {
        return fileSize switch
        {
            FileSize.Small => SmallFileProcessor(),
            FileSize.Medium => MediumFileProcessor(),
            FileSize.Large => LargeFileProcessor(),
            _ => null,
        };
    }
}

