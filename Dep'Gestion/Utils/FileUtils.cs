using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    public class FileUtils
    {
        public static void createFile(string path)
        {
            System.IO.File.Create(path);
        }

        public static string getCurrentDirectory()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }

        public static void createRandomTxtFileInCurrentDirectory()
        {
            string path = FileUtils.getCurrentDirectory();
            path += "\\" + Outils.generateRandomString() + ".txt";
            FileUtils.createFile(path);
        }
    }
}