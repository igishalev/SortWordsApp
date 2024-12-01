using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using SortWordsApp.Interfaces;
using SortWordsApp.Processor;
using SortWordsApp.Readers;

namespace SortWordsTest;

public class E2EMaxOccurrences
{
    private readonly IConfiguration _configuration;
    private string ThreeMaxDuplicates;
    private string TwoMaxDuplicates;
    private string OneMaxOccurrence;
    private string AllWordsAppearOnce;

    public E2EMaxOccurrences()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("C:\\Users\\Igalsh\\source\\repos\\WordSortApp\\appsettings.json")
            .Build();

        ThreeMaxDuplicates = _configuration.GetSection("ThreeMaxDuplicates").Value;
        TwoMaxDuplicates = _configuration.GetSection("TwoMaxDuplicates").Value;
        OneMaxOccurrence = _configuration.GetSection("OneMaxOccurrnce").Value;
        AllWordsAppearOnce = _configuration.GetSection("AllWordsAppearOnce").Value;
    }

    [Test]
    public void RunFlowTests(
        [Values] FilePathsOccurrencesTests path,
        [Values(FileSize.Small, FileSize.Medium, FileSize.Large)] FileSize size
        )
    {
        RunFlow(path, size);
    }

    private void RunFlow(FilePathsOccurrencesTests path, FileSize size)
    {
        IProcessor processor = new ProcessorStrategy(true).GetFileProcessor(size);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Console.WriteLine("Start processing file");
        var mostFrequentWords = processor.Process(GetFilePath(path), "F2.txt");
        sw.Stop();
        Console.WriteLine("Stop processing file");
        Console.WriteLine("elapsed" + sw.Elapsed);
        mostFrequentWords.ForEach((kvp) =>
                                    Console.WriteLine("Most frequent word: " + kvp.Key + " Count: " + kvp.Value));
        ValidateResults(mostFrequentWords, path);
    }

    private string GetFilePath(FilePathsOccurrencesTests path)
    {
        return path switch
        {
            FilePathsOccurrencesTests.ThreeMaxDuplicates => ThreeMaxDuplicates,
            FilePathsOccurrencesTests.TwoMaxDuplicates => TwoMaxDuplicates,
            FilePathsOccurrencesTests.OneMaxOccurrence => OneMaxOccurrence,
            FilePathsOccurrencesTests.AllWordsDistinct => AllWordsAppearOnce,
            _ => throw new ArgumentException("Invalid file name", nameof(path))
        };
    }

    private int GetNumberOfMaxOccurrences(FilePathsOccurrencesTests path)
    {
        return path switch
        {
            FilePathsOccurrencesTests.ThreeMaxDuplicates => 3,
            FilePathsOccurrencesTests.TwoMaxDuplicates => 2,
            FilePathsOccurrencesTests.OneMaxOccurrence => 1,
            FilePathsOccurrencesTests.AllWordsDistinct => 20,
            _ => throw new ArgumentException("Invalid file name", nameof(path))
        };
    }

    private bool GetSortOrder(string direction)
    {
        Console.WriteLine("Enter sort option: 'a' for ascending, 'd' for descending:");
        return direction == "a";
    }

    private void ValidateResults(List<KeyValuePair<string, long>> mostFrequentWords, FilePathsOccurrencesTests inFile)
    {
        var outputFileName = "F2.txt";
        Assert.That(File.Exists(outputFileName));
        var sortedWords = File.ReadAllText(Path.GetFullPath(outputFileName))
            .Split(new[] { ' ', '\r', '\n', ',', '.', '!', '?', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

        Assert.That(sortedWords.Count(), Is.EqualTo(sortedWords.Distinct().Count()));
        Assert.That(mostFrequentWords.Count, Is.EqualTo(GetNumberOfMaxOccurrences(inFile)));
    }
}