using System.IO;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime[] test = get_date();
            Console.WriteLine(test);
        }
    }
}