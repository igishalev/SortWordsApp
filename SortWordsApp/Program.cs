using SortWordsApp.Processor;
using SortWordsApp.Readers;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var outputFilePath = Path.GetFullPath("F2.txt");

            if (args.Length != 1)
            {
                Console.WriteLine("Please Use: SortWordsApp.exe <path-to-file>");
                return;
            }
            var inputFilePath = args[0];
            var size = new FileEvaluator().EvaluateFile(inputFilePath);
            bool isAscending = WaitForInputOption();

            var mostFrequentWords = new ProcessorStrategy(isAscending)
                .GetFileProcessor(size)
                .Process(inputFilePath, outputFilePath);

            mostFrequentWords.ForEach(PrintMostFrequentWords);
            Console.WriteLine($"Words have been sorted and written to {outputFilePath}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Try Running Again with the absolute file path");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    private static void PrintMostFrequentWords(KeyValuePair<string, long> frequentWord)
    {
        Console.WriteLine("Most frequent word: " + frequentWord.Key + "\nCount: " + frequentWord.Value);
    }

    private static bool WaitForInputOption()
    {
        string userSortOption;
        Console.WriteLine("Enter sort option: 'a' for ascending, 'd' for descending:");
        while (true)
        {
            userSortOption = Console.ReadLine()?.Trim();

            if (userSortOption is "a" or "d")
            {
                break;
            }

            Console.WriteLine("Invalid sort option. Please enter 'a' for ascending or 'd' for descending.");
        }

        return userSortOption == "a"; ;
    }
}