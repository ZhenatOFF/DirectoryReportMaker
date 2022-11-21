using DirectoryReportMaker;
using System.Reflection;

class Program
{
    static void Main()
    {
        //получаем путь к родительской директории, в которой находится исполняемый файл
        string path = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName; 

        HtmlReportMaker.Save("result.html", path);
    }
}