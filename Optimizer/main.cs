using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web;

namespace Optimizer
{
    class main
    {

        enum RecycleFlags : uint
        {
            SHRB_NOCONFIRMATION = 0x00000001,
            SHRB_NOPROGRESSUI = 0x00000002,
            SHRB_NOSOUND = 0x00000004
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        static void Main(string[] args)
        {
            
            string appTemp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp";
            string winTemp = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Temp";

            string[] appfile = Directory.GetFiles(appTemp);
            string[] appFolder = Directory.GetDirectories(appTemp);
            string[] winappfile = Directory.GetFiles(winTemp);
            string[] winappfolder = Directory.GetDirectories(winTemp);

            // AppTemp Function

            foreach (string f in appfile)
            {
                try
                {
                    File.Delete(f);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (string f in appFolder)
            {
                try
                {
                    Directory.Delete(f, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // WindowTemp Function

            foreach (string f in winappfile)
            {
                try
                {
                    File.Delete(f);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (string f in winappfolder)
            {
                try
                {
                    Directory.Delete(f, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Recycle Bin Function

            Console.WriteLine("Are you sure you want to delete all the items in recycle bin?");
            Console.WriteLine("Y or N");
            string choose = Console.ReadLine();

            if(choose == "Y")
            {
                try
                {
                    uint IsSuccess = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHRB_NOCONFIRMATION);
                    Console.WriteLine("The recycle bin has been succesfully recycled !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Chrome Cache, History Clear

            Console.WriteLine("Are you sure you want to delete all caches?");
            Console.WriteLine("Y or N");
            choose = Console.ReadLine();

            string chromeData_location = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default";
            string cachePath = chromeData_location + @"\Cache\Cache_Data";
            string[] c_cache = Directory.GetFiles(cachePath);

            if(choose == "Y")
            {
                foreach (string cache in c_cache)
                {
                    File.Delete(cache);
                }
            }

            Console.WriteLine("Want to clear History?");
            Console.WriteLine("Y or N");
            choose = Console.ReadLine();
            if(choose == "Y")
            {
                File.Delete(chromeData_location+@"\History");
            }

            string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            string[] downloadFiles = Directory.GetFiles(downloadPath);

            Console.WriteLine("Want to clear Download Folder?");
            Console.WriteLine("Y or N");
            choose = Console.ReadLine();

            if(choose == "Y")
            {
                foreach(string download in downloadFiles)
                {
                    File.Delete(download);
                }
            }
        }
    }
}
