using System.Diagnostics;

namespace OtusTplHomeWork
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            ParallelFileReader reader = new ParallelFileReader();
            reader.ReadCompletedEventHandler += DisplayReadFileResult;
            sw.Start();
            await reader.ReadAllFromDirectory("Txt");
            sw.Stop();
            Console.WriteLine($"Общее время выполнения операции - {sw.Elapsed}");            
        }
        static void DisplayReadFileResult(ParallelFileReader sender, ReadCompletedEventArgs e)
        {
            Console.WriteLine($"Фаил {Path.GetFileName(e.FilePath)} прочитан. Количество пробелов {e.SpaceCount}. Время чтения {e.Time}");
        }
    }
}
