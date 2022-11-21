using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReportMaker
{
    public static class HtmlReportMaker
    {
        public static void Save(string savePath, string directoryPath)
        {
            var fileList = DirectoryAnalyzer.GetAllFoldersAndFilesUnder(directoryPath); //получаем список всех файлов и поддиректорий
            var mimeTypeStatistics = DirectoryAnalyzer.GetMimeTypeStatistics(directoryPath); //получаем статистику по MimeType в директории

            if (fileList == null)
                return;

            FileStream fileStream = File.Open(savePath, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                //выводим таблицу со статистикой по MimeType
                writer.WriteLine("Статистика по MimeType в директории:");
                writer.WriteLine("<table border='1'>");

                writer.WriteLine("<tr>");

                writer.WriteLine($"<th>MimeType</th>");
                writer.WriteLine($"<th>Количество</th>");
                writer.WriteLine($"<th>Доля, %</th>");
                writer.WriteLine($"<th>Средний размер, Б</th>");

                writer.WriteLine("</tr>");

                foreach (MimeTypeStatistics stat in mimeTypeStatistics)
                {
                    writer.WriteLine("<tr>");

                    writer.WriteLine($"<td>{stat.Extension}</td>");
                    writer.WriteLine($"<td>{stat.Count}</td>");
                    writer.WriteLine("<td>{0:0.00}</td>", stat.Percentage);
                    writer.WriteLine($"<td>{stat.AverageSize}</td>");

                    writer.WriteLine("</tr>");
                }

                writer.WriteLine("</table>");

                //выводим вложенный список всех поддиректорий и файлов
                writer.WriteLine($"\nРезультат сканирования директории:{Path.GetFullPath(directoryPath)}"); 
                foreach (string file in fileList)
                {
                    writer.WriteLine(file);
                }

                writer.Close();
            }
        }


    }
}
