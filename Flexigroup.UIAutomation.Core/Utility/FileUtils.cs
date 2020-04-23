using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexigroup.UIAutomation.Core
{
    public static class FileUtils
    {
        public static string CreateFile(string fileDir, string fileName)
        {
            var fileDirLocation = fileDir;
            var createDir = Directory.CreateDirectory(fileDirLocation);
            var logFile = fileDirLocation + fileName;
            return logFile;
        }
        
        public static void AppendToFile(string file, string input)
        {
            using (StreamWriter w = File.AppendText(file))
            {
                w.WriteLine(input);
            }
        }
        


    }
}
