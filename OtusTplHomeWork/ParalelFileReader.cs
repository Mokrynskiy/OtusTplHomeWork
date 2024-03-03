using System.Diagnostics;

namespace OtusTplHomeWork
{
    public class ParallelFileReader
    {
        public delegate void ReadComplit(ParallelFileReader sender, ReadCompletedEventArgs e);
        public event ReadComplit? ReadCompletedEventHandler;

        public async Task ReadAllFromDirectory(string folderPath)
        {
            if(Directory.Exists(folderPath))
            {
                await ReadFromFilePathList(Directory.GetFiles(folderPath).ToList());
            }
        }
        public async Task ReadFromFilePathList(List<string> filePathList)
        {
            List<Task> tasks = new List<Task>();
            foreach (string filePath in filePathList)
            { 
                tasks.Add(Task.Factory.StartNew(() => ReadSpaceCountFromFile(filePath)));
            }
            Task.WaitAll(tasks.ToArray());
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
                ReadCompletedEventHandler?.Invoke(this, new ReadCompletedEventArgs { FilePath = filePath, SpaceCount = spaceCount, Time = sw.Elapsed });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
    public class ReadCompletedEventArgs: EventArgs
    {
        public string FilePath { get; set; }
        public long SpaceCount { get; set; }
        public TimeSpan Time { get; set; }
    }
}
