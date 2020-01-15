using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ListenerClass
{
    class Program
    {

        static void Main(string[] args)
        {
            string directory = @"C:\ListenerPath\";
            
            Console.WriteLine("****************************************");
            Console.WriteLine("*******    L I S T E N E R   ***********");
            Console.WriteLine("****************************************");
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = directory;
                watcher.NotifyFilter = NotifyFilters.CreationTime
                                     | NotifyFilters.CreationTime
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                watcher.Filter = "*.txt";

                //watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                //watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press 'q' to quit the watcher");
                while (Console.Read() != 'q') ;
            }

        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File: {e.FullPath} renamed/created to {e.ChangeType}");
            Console.WriteLine("The new file will be processed");
            string[] lines = File.ReadAllLines(e.FullPath);

            Random rdm = new Random();
            int newFileName = rdm.Next(1000, 9999);
            Console.WriteLine("The new file was created in: " + @"C:\NewListenerCreated\File_" + newFileName.ToString() + ".txt");
            FileStream fs = File.Create(@"C:\NewListenerCreated\File_" + newFileName.ToString() + ".txt");
            fs.Close();
            int countLine = 0;


            using (StreamWriter outputFile = new StreamWriter(@"C:\NewListenerCreated\File_" + newFileName.ToString() + ".txt", true))
            {
                foreach (string line in lines)
                {
                    if (countLine <= 2)
                    {
                        outputFile.WriteLine(line.ToString());
                    }
                    else if(line.Contains("MGR"))
                    {
                        Console.WriteLine("The expression was added int the new file: " + line.ToString());
                        outputFile.WriteLine(line.ToString());
                    }
                    countLine++;
                }
                Console.WriteLine("The new was processed");
            }

        }


        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
        }
        
    }
}
