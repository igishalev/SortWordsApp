using SortWordsApp.Interfaces;

namespace SortWordsApp.Writers;

public class SmallWriter : IFileWriter
{
    public void WriteFile(string outputFilePath, IEnumerable<string> container)
    {
        try
        {
            File.WriteAllText(outputFilePath, string.Join(Environment.NewLine, container));
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred while writing to the file: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"You do not have permission to write to the file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}