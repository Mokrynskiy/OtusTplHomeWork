namespace OtusTplHomeWork
{
    public class Program
    {
        static void Main(string[] args)
        {            
            //List<string> filePathList = new List<string> { "Txt\\Book1.txt", "Txt\\Book2.txt", "Txt\\Book3.txt" };
            ParalelFileReader reader = new ParalelFileReader();
            reader.ReadComplitEventHandler += DisplayReadFileResult;
            
            //reader.ReadFromFilePathList(filePathList);
            
            reader.ReadAllFromDirectory("Txt");
            Console.ReadKey();
        }
        static void DisplayReadFileResult(ParalelFileReader sender, ReadComplitEventArgs e)
        {
            Console.WriteLine($"Фаил {Path.GetFileName(e.FilePath)} прочитан. Количество пробелов {e.SpaceCount}. Время чтения {e.Time}");
        }
    }
}
