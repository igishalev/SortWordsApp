using System.Text;
using SortWordsApp.Interfaces;

namespace SortWordsApp.Writers;

public class LargerWriter : IFileWriter
{
    public void WriteFile(string outputFilePath, IEnumerable<string> container)
    {
        const int bufferSize = 8192; // 8 KB buffer size
        try
        {
            using var writer = new StreamWriter(outputFilePath, false, Encoding.UTF8, bufferSize);
            var words = new List<string>();
            foreach (var word in container)
            {
                words.Add(word);
                if (words.Count >= bufferSize)
                {
                    writer.WriteLine(string.Join(Environment.NewLine, words));
                    words.Clear();
                }
            }
            if (words.Count > 0)
            {
                writer.WriteLine(string.Join(Environment.NewLine, words));
            }
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

