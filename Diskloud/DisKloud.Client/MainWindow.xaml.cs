using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DisKloud.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32", SetLastError = true)]
        public static extern void FreeConsole();

        public MainWindow()
        {
            InitializeComponent();
            AllocConsole();
        }

        public DateTime[] get_date()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*", SearchOption.AllDirectories);
            DateTime[] dates = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++){
                dates[i] = File.GetLastWriteTime(files[i]);
            }
            return dates;

        }

        public void save_file_id()
        {
            if (File.Exists("file_ids.txt"))
            {
                Console.WriteLine("Deleting old file");
                File.Delete("file_ids.txt");
            }
            else
            {
                Console.WriteLine("Creating new file.");
            }
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                File.AppendAllText("file_ids.txt", i + ";" + Directory.GetCurrentDirectory() + ";" + files[i].Replace(Directory.GetCurrentDirectory() + "\\" ,"") + "\n");
                Console.WriteLine("done");
            }
        }

        public void get_file_id()
        {
            string[] file;
            StreamReader reader = File.OpenText("file_ids.txt");
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.IndexOf(";", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    file = line.Split(";");
                    for (int i = 0; i < file.Length; i++)
                    {
                        Console.WriteLine(file[i]);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            save_file_id();
        }
    }
}