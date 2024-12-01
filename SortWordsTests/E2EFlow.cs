using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using SortWordsApp.Interfaces;
using SortWordsApp.Processor;
using SortWordsApp.Readers;

namespace SortWordsTest;

public class E2EFlow
{
    private readonly IConfiguration _configuration;
    private string filePathF10MB;
    private string filePathF100KLines;
    private string filePathF10KLines;
    private string filePathF1;

    public E2EFlow()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("C:\\Users\\Igalsh\\source\\repos\\WordSortApp\\appsettings.json")
            .Build();

        filePathF10MB = _configuration.GetSection("FileInputPath10MB").Value;
        filePathF100KLines = _configuration.GetSection("FileInputPath100K").Value;
        filePathF10KLines = _configuration.GetSection("FileInputPath10K").Value;
        filePathF1 = _configuration.GetSection("FileInputPath").Value;
    }

    [Test]
    public void RunFlowTests(
        [Values] FilePathE2EFlowTests path,
        [Values(FileSize.Small, FileSize.Medium, FileSize.Large)] FileSize size,
        [Values] bool isAscending)
    {
        RunFlow(path, size, isAscending);
    }

    private void RunFlow(FilePathE2EFlowTests path, FileSize size, bool isAscending)
    {
        IProcessor processor = new ProcessorStrategy(isAscending).GetFileProcessor(size);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Console.WriteLine("Start processing file");
        var mostFrequentWords = processor.Process(GetFilePath(path), "F2.txt");
        sw.Stop();
        Console.WriteLine("Stop processing file");
        Console.WriteLine("elapsed" + sw.Elapsed);
        mostFrequentWords.ForEach((kvp) =>
                                    Console.WriteLine("Most frequent word: " + kvp.Key + " Count: " + kvp.Value));
        ValidateResults(mostFrequentWords.FirstOrDefault(), path, isAscending);
    }

    private string GetFilePath(FilePathE2EFlowTests path)
    {
        return path switch
        {
            FilePathE2EFlowTests.TenMB => filePathF10MB,
            FilePathE2EFlowTests.OneHundredK => filePathF100KLines,
            FilePathE2EFlowTests.TenK => filePathF10KLines,
            FilePathE2EFlowTests.FileOne => filePathF1,
            _ => throw new ArgumentException("Invalid file name", nameof(path))
        };
    }


    private void ValidateResults(KeyValuePair<string, long> mostFrequent, FilePathE2EFlowTests inputFileName, bool isAscending)
    {
        var outputFileName = "F2.txt";
        Assert.That(File.Exists(outputFileName));

        var sortedWords = File.ReadAllText(Path.GetFullPath(outputFileName))
            .Split(new[] { ' ', '\r', '\n', ',', '.', '!', '?', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);
        Assert.That(sortedWords.Count(), Is.EqualTo(sortedWords.Distinct().Count()));

        if (isAscending)
        {
            Assert.That(sortedWords.First().First(), Is.EqualTo('a').Or.EqualTo('1'));
        }
        else
        {
            Assert.That(sortedWords.First().First(), Is.EqualTo('w').Or.EqualTo('y'));
        }
        if (FilePathE2EFlowTests.TenMB != inputFileName)
        {
            Assert.That(mostFrequent.Key, Is.EqualTo("Some".ToLower()));
        }
        else
        {
            Assert.That(mostFrequent.Key, Is.EqualTo("the"));
        }

        if (FilePathE2EFlowTests.FileOne == inputFileName)
            Assert.That(mostFrequent.Value, Is.EqualTo(6));
        if (FilePathE2EFlowTests.TenMB == inputFileName)
            Assert.That(mostFrequent.Value, Is.EqualTo(154900));
    }
}
