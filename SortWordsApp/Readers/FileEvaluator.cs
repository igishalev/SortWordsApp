using Microsoft.Extensions.Configuration;

namespace SortWordsApp.Readers;

public class FileEvaluator
{
    private readonly long _smallFileThreshold;
    private readonly long _largeFileThreshold;

    public FileEvaluator()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var smallFileThreshold = configuration.GetSection("FileSizeThresholds:SmallFileThreshold").Value;
        _smallFileThreshold = Convert.ToInt64(smallFileThreshold);
        var largeFileThreshold = configuration.GetSection("FileSizeThresholds:LargeFileThreshold").Value;
        _largeFileThreshold = Convert.ToInt64(largeFileThreshold);
    }

    public FileSize EvaluateFile(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("Error: File not found.");
            throw new FileNotFoundException("Try Running Again with the absolute file path");
        }
        try
        {
            long fileSizeInBytes = new FileInfo(inputFilePath).Length;

            if (fileSizeInBytes < _smallFileThreshold)
            {
                return FileSize.Small;
            }
            else if (fileSizeInBytes < _largeFileThreshold)
            {
                return FileSize.Medium;
            }
            else
            {
                return FileSize.Large;
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: Access to the file is denied.");
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Error: The file path is too long.");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: The directory was not found.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: An I/O error occurred. {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: An unexpected error occurred. {ex.Message}");
        }
        return FileSize.Empty;
    }
}