using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace DisKloud.Client.Core
{
    class Worker
    {

        private ApiRequestManager manger;
        private bool running = true;
        public Worker(string server) 
        {
            manger = new ApiRequestManager(server);
        }
        public void work()
        {


            try
            {

               // if (!startConnection()) throw new Exception("Connection error");




                while (running)
                {

                    Console.WriteLine("Working");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("stopping...");

            }
            catch (Exception ex)
            {


                Console.WriteLine($"error : {ex}");
            }
        }




        private bool startConnection()
        {
            LogIn dialog = new LogIn();

            if (dialog.ShowDialog() == true)
            {
                Console.WriteLine("Connectiong...");
                var tasktest = manger.GetApiKey(dialog.Result[0], dialog.Result[1]);
                tasktest.Wait();
                if (!tasktest.Result) throw new Exception("Connection error");
                return true;
            }
            return false;
        }

        public void r()=> running=false; 


    }
}
