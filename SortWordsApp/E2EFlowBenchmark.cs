using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Configuration;
using SortWordsApp.Interfaces;
using SortWordsApp.Processor;
using SortWordsApp.Readers;

namespace SortWordsApp.Benchmarks;

//Todo : Uncomment the following code to run the benchmark
/*    public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<E2EFlowBenchmark>();
    }
}*/

public class E2EFlowBenchmark
{
    private string filePathF10MB;
    private string filePathF100K;
    private string filePathF10K;
    private string filePathF1;

    [GlobalSetup]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("C:\\Users\\Igalsh\\source\\repos\\WordSortApp\\appsettings.json")
            .Build();
        filePathF10MB = configuration.GetSection("FileInputPath10MB").Value;
        filePathF100K = configuration.GetSection("FileInputPath100K").Value;
        filePathF10K = configuration.GetSection("FileInputPath10K").Value;
        filePathF1 = configuration.GetSection("FileInputPath").Value;
    }

    [Benchmark]
    [Arguments("F1.txt", FileSize.Large)]
    [Arguments("F10K.txt", FileSize.Large)]
    [Arguments("F100K.txt", FileSize.Large)]
    [Arguments("F10MB.txt", FileSize.Large)]

    [Arguments("F1.txt", FileSize.Medium)]
    [Arguments("F10K.txt", FileSize.Medium)]
    [Arguments("F100K.txt", FileSize.Medium)]
    [Arguments("F10MB.txt", FileSize.Medium)]

    [Arguments("F10MB.txt", FileSize.Small)]
    [Arguments("F100K.txt", FileSize.Small)]
    [Arguments("F10K.txt", FileSize.Small)]
    [Arguments("F1.txt", FileSize.Small)]
    public void RunFlowBenchmark(string filePath, FileSize fileSize)
    {
        RunFlow(filePath, fileSize);
    }

    private string GetFilePath(string fileName)
    {
        return fileName switch
        {
            "F10MB.txt" => filePathF10MB,
            "F100K.txt" => filePathF100K,
            "F10K.txt" => filePathF10K,
            "F1.txt" => filePathF1,
            _ => throw new ArgumentException("Invalid file name", nameof(fileName))
        };
    }

    private void RunFlow(string filePath, FileSize fileSize)
    {
        var strategy = new ProcessorStrategy(true);
        IProcessor processor = strategy.GetFileProcessor(fileSize);
        processor.Process(filePath, "F2.txt");
    }
}
