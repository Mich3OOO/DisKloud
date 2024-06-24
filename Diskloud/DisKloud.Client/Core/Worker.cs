using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace DisKloud.Client.Core
{
    class Worker
    {

        private ApiRequestManager Manger;
        private string ConfigFilePath;
        private string DataFilePath = "./Config/data";
        private string[] MonitoredDirs = { "C:\\Users\\Mich3000\\Documents\\testdirforDiskloud" };
        private Dictionary<Guid, FileData> FilesData = new Dictionary<Guid, FileData>();
        public bool running { get; set; } = true;



        public Worker(string server) 
        {
            Manger = new ApiRequestManager(server);
            Directory.CreateDirectory(DataFilePath);
        }
        public void Work()
        {


            try
            {

               if (!StartConnection()) throw new Exception("Connection error");




                while (running)
                {

                    Console.WriteLine("Working");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("stopping...");
                SaveData();

            }
            catch (Exception ex)
            {


                Console.WriteLine($"error : {ex}");
            }
        }





        private bool StartConnection()
        {
            LogIn dialog = new LogIn();

            if (dialog.ShowDialog() == true)
            {
                Console.WriteLine("Connectiong...");
                var tasktest = Manger.GetApiKey(dialog.Result[0], dialog.Result[1]);
                tasktest.Wait();
                if (!tasktest.Result) throw new Exception("Connection error");
                return true;
            }
            return false;
        }
        private void UpdateLocalData()
        {

        }
        private void CompareData()
        {

        }


        private Dictionary<Guid, FileData> GetLocalData()
        {
            Dictionary<Guid, FileData> r = new Dictionary<Guid, FileData>();

            List<string> directorysToChek = new List<string>(MonitoredDirs);

            string currentDir;

            while (directorysToChek.Count>0)
            {
                currentDir = directorysToChek.Last() + "\\";
                directorysToChek.RemoveAt(directorysToChek.Count -1);
                directorysToChek.AddRange(Directory.GetDirectories(currentDir));
                foreach (string name in Directory.GetFiles(currentDir))
                {
                    r.Add(Guid.Empty, new FileData(name.Replace(currentDir, ""), currentDir, File.GetLastWriteTimeUtc(name)));
                }



            }

            return r;

        }



        public Dictionary<Guid, FileData> get_file_id()
        {
            string[] file;
            string line;
            StreamReader reader = File.OpenText(DataFilePath);
            Dictionary < Guid, FileData > result = new Dictionary<Guid, FileData >();



            while ((line = reader.ReadLine()) != null)
            {
                if (line.IndexOf(";", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    file = line.Split(";");

                    result.Add(Guid.Parse(file[0]), new FileData(file[1], file[2], DateTime.Parse(file[3])));
                }
            }
            reader.Close();
            return result;
        }
        private void SaveData()
        {
            if (File.Exists(DataFilePath)) File.Delete("file_ids.txt");
            
            foreach (Guid key in FilesData.Keys)
            {
                File.AppendAllText(DataFilePath, $"{key.ToString()};{FilesData[key].Name};{FilesData[key].Path};{FilesData[key].VersionDate.ToString()}\n");
            }
        }

    }
}
