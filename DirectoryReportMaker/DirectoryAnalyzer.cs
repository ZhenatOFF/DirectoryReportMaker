using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReportMaker
{
    public static class DirectoryAnalyzer
    {
        public static List<string> GetAllFoldersAndFilesUnder(string path)
        {
            List<string> ls = new List<string>();

            var directory = new DirectoryInfo(path);
            try
            {

                ls.Add("<ul>");

                //получаем список всех директорий, рекурсивно пробегаемся по каждой
                foreach (DirectoryInfo folder in directory.GetDirectories())   
                {
                    ls.Add($"<li>{folder.Name} {GetDirectorySize(folder)} Б</li>");
                    ls.AddRange(GetAllFoldersAndFilesUnder(folder.FullName));
                }
                FileInfo[] files = directory.GetFiles(); //получаем список всех файлов в директории(поддиректории)
                foreach (FileInfo file in files)
                {
                    ls.Add($"<li>{file.Name} {file.Length}Б {file.Extension.Remove(0, 1).ToUpper()}</li>");
                }

                ls.Add("</ul>");
            }
            catch (System.Exception e)
            {
                //Обработка исключения
            }
            return ls;
        }


        public static long GetDirectorySize(DirectoryInfo d)  
        {
            long size = 0;

            FileInfo[] files = d.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            DirectoryInfo[] directories = d.GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                size += GetDirectorySize(directory);
            }
            return size;
        }

        public static IEnumerable GetMimeTypeStatistics(string path)
        {
            var directory = new DirectoryInfo(path);

            FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories); //получаем список со всем файлами в директории

            var stats = files                           //создаем список со статистикой по каждому MimeType(расширение, количественное и процентное соотношение, средний размер)
                .Select(file => new MimeTypeStatistics                  
                {
                    Extension = file.Extension,
                    Count = files.Where(f => f.Extension == file.Extension).Count(),
                    Percentage = (double)files.Where(f => f.Extension == file.Extension).Count() / files.Count() * 100,
                    AverageSize = (long)files.Where(f => f.Extension == file.Extension).Average(f => f.Length)
                })
                .DistinctBy(file => file.Extension)
                .OrderByDescending(file => file.Count);

            return stats;
        }
    }


}
