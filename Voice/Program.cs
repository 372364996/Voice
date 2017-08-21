using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Voice
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //ffmpeg - i test.mp3 - qscale 0 out.wav
            //    sox out.wav - n noiseprof noise.prof
            //    sox out.wav out-clean.wav noisered noise.prof 0.21
            //ffmpeg - i out-clean.wav new.mp3
            //C:\Program Files (x86)\sox-14-4-2
            var tempDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");
            const string arg = "-i 91.mp3 -qscale 0 out.wav";
            var info = new ProcessStartInfo("ffmpeg.exe", arg)
            {
                CreateNoWindow = true,//cmd != "push",
                UseShellExecute = false,
                WorkingDirectory = tempDir,
            };
            var processWorker = Process.Start(info);
            if (processWorker != null)
            {
                processWorker.WaitForExit();
                processWorker.Close();
            }
            Console.WriteLine("ffmpeg运行完成");
            const string arg2 = "out.wav -n noiseprof noise.prof";
            var info2 = new ProcessStartInfo(@"C:\Program Files (x86)\sox-14-4-2\sox.exe", arg2)
            {
                CreateNoWindow = true,//cmd != "push",
                UseShellExecute = false,
                WorkingDirectory = tempDir,
            };
            var processWorker2 = Process.Start(info2);
            if (processWorker2 != null)
            {
                processWorker2.WaitForExit();
                processWorker2.Close();
            }
         
            Console.WriteLine("sox运行完成");
            const string arg3 = "out.wav out-clean.wav noisered noise.prof 0.21";
            var info3= new ProcessStartInfo(@"C:\Program Files (x86)\sox-14-4-2\sox.exe", arg3)
            {
                CreateNoWindow = true,//cmd != "push",
                UseShellExecute = false,
                WorkingDirectory = tempDir,
            };
            var processWorker3 = Process.Start(info3);
            if (processWorker3 != null)
            {                
                processWorker3.WaitForExit();
                processWorker3.Close();
            }
           
            Console.WriteLine("sox运行完成");
            const string arg4 = "-i out-clean.wav new.mp3";
            var info4 = new ProcessStartInfo("ffmpeg.exe", arg4)
            {
                CreateNoWindow = true,//cmd != "push",
                UseShellExecute = false,
                WorkingDirectory = tempDir,
            };
            var processWorker4 = Process.Start(info4);
            if (processWorker4 != null)
            {                
                processWorker4.WaitForExit();
                processWorker4.Close();
            }
            byte[] buffer = File.ReadAllBytes(Path.Combine(tempDir, "out-clean.wav"));
            Console.WriteLine(buffer.ToString());
            Console.WriteLine("ffmoeg运行完成");
            //File.Delete(Path.Combine(tempDir, "out.wav"));
            //Console.WriteLine("删除out.wav");
            //File.Delete(Path.Combine(tempDir, "noise.prof"));
            //Console.WriteLine("删除noise.prof");
            //File.Delete(Path.Combine(tempDir, "out-clean.wav"));
            //Console.WriteLine("删除noise.prof");
            //File.Delete(Path.Combine(tempDir, "new.mp3"));
            //Console.WriteLine("删除new.mp3");
            Console.WriteLine("Hello!~");
            string str=   stopwatch.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            Console.WriteLine("耗时："+str);

        }
    }
}
