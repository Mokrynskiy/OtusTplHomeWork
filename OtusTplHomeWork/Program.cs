using System.Diagnostics;

namespace OtusTplHomeWork
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            ParallelFileReader reader = new ParallelFileReader();
            reader.ReadCompletedEventHandler += DisplayReadFileResult;
            sw.Start();
            var task = reader.ReadAllFromDirectory("Txt");
            if (task.Exception != null)
            {
                Console.WriteLine(task.Exception.Message);
            }
            sw.Stop();
            Console.WriteLine($"Общее время выполнения операции - {sw.Elapsed}");            
        }
        static void DisplayReadFileResult(ParallelFileReader sender, ReadCompletedEventArgs e)
        {
            Console.WriteLine($"Фаил {Path.GetFileName(e.FilePath)} прочитан. Количество пробелов {e.SpaceCount}. Время чтения {e.Time}");
        }
    }
}
