using System.Diagnostics;

namespace OtusTplHomeWork
{
    public class ParalelFileReader
    {
        public delegate void ReadComplit(ParalelFileReader sender, ReadComplitEventArgs e);
        public event ReadComplit? ReadComplitEventHandler;

        public void ReadAllFromDirectory(string folderPath)
        {
            if(Directory.Exists(folderPath))
            {
                ReadFromFilePathList(Directory.GetFiles(folderPath).ToList());
            }
        }
        public void ReadFromFilePathList(List<string> filePathList)
        {
            foreach (string filePath in filePathList)
            {
                if (File.Exists(filePath))
                {
                    Task.Run(() => ReadSpaceCountFromFile(filePath));
                }
            }
        }
        private void ReadSpaceCountFromFile(string filePath)
        {
            long spaceCount = 0;
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                using(var sr = new StreamReader(filePath))
                {
                    foreach (var item in sr.ReadToEnd())
                    {
                        if (item == ' ') spaceCount++;                        
                    }
                }
                sw.Stop();
                ReadComplitEventHandler?.Invoke(this, new ReadComplitEventArgs { FilePath = filePath, SpaceCount = spaceCount, Time = sw.Elapsed });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    
    public class ReadComplitEventArgs: EventArgs
    {
        public string FilePath { get; set; }
        public long SpaceCount { get; set; }
        public TimeSpan Time { get; set; }
    }
}
